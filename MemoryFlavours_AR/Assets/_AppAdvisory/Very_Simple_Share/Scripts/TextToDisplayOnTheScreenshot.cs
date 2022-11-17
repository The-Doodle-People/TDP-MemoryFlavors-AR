using UnityEngine;
using System.Collections;

namespace AppAdvisory.SharingSystem
{
	public class TextToDisplayOnTheScreenshot : MonoBehaviour 
	{
		public void SetTextToDisplayOnTheScreenshot(string text)
		{
			GetComponent<UnityEngine.UI.Text>().text = text;
		}
	}
}
