using UnityEngine;
using System;

namespace AppAdvisory
{
	public class OpenMoreGames : MonoBehaviour 
	{
		public void OnClicked()
		{
			Application.OpenURL("http://u3d.as/9cs");
		}
	}
}