using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using CW.Common;

namespace Lean.Texture
{
	/// <summary>This allows you to multiply all pixels in the current texture by the specified value, either to all RGBA channels, or individually.</summary>
	[System.Serializable]
	public class LeanFilterMultiply : LeanFilter
	{
		public override string Title
		{
			get
			{
				return "Multiply Channels";
			}
		}

		public enum ChannelType
		{
			AllTheSame,
			Individual
		}

		/// <summary>How should the texture channels should be modified?</summary>
		public ChannelType Channels { set { channels = (int)value; } get { return (ChannelType)channels; } } [SerializeField] [LeanEnum(typeof(ChannelType))] private int channels;

		/// <summary>All channels will be multiplied by this value.</summary>
		public float Multiplier { set { multiplier = value; } get { return multiplier; } } [SerializeField] private float multiplier = 1.0f;

		/// <summary>This channel value will be multiplied by this value.</summary>
		public float MultiplierR { set { multiplierR = value; } get { return multiplierR; } } [SerializeField] private float multiplierR = 1.0f;

		/// <summary>This channel value will be multiplied by this value.</summary>
		public float MultiplierG { set { multiplierG = value; } get { return multiplierG; } } [SerializeField] private float multiplierG = 1.0f;

		/// <summary>This channel value will be multiplied by this value.</summary>
		public float MultiplierB { set { multiplierB = value; } get { return multiplierB; } } [SerializeField] private float multiplierB = 1.0f;

		/// <summary>This channel value will be multiplied by this value.</summary>
		public float MultiplierA { set { multiplierA = value; } get { return multiplierA; } } [SerializeField] private float multiplierA = 1.0f;

		[BurstCompile]
		struct FilterJob : IJobParallelFor
		{
			public NativeArray<float4> Pixels;

			[ReadOnly] public float4 Multiplier;

			public void Execute(int index)
			{
				var pixel = Pixels[index];

				pixel *= Multiplier;

				Pixels[index] = pixel;
			}
		}

		public override void Schedule(LeanPendingTexture data)
		{
			var filter = new FilterJob();

			filter.Pixels = data.Pixels;

			if (Channels == ChannelType.Individual)
			{
				filter.Multiplier.x = multiplierR;
				filter.Multiplier.y = multiplierG;
				filter.Multiplier.z = multiplierB;
				filter.Multiplier.w = multiplierA;
			}
			else
			{
				filter.Multiplier = multiplier;
			}

			data.Handle = filter.Schedule(data.Pixels.Length, 32, data.Handle);
		}

#if UNITY_EDITOR
		protected override void DrawInspector()
		{
			CwEditor.Draw("channels", "How should the texture channels should be modified?");
			if (CwEditor.GetProperty("channels").intValue == (int)ChannelType.Individual)
			{
				CwEditor.Draw("multiplierR", "This channel value will be multiplied by this value.");
				CwEditor.Draw("multiplierG", "This channel value will be multiplied by this value.");
				CwEditor.Draw("multiplierB", "This channel value will be multiplied by this value.");
				CwEditor.Draw("multiplierA", "This channel value will be multiplied by this value.");
			}
			else
			{
				CwEditor.Draw("multiplier", "All channels will be multiplied by this value.");
			}
		}
#endif
	}
}