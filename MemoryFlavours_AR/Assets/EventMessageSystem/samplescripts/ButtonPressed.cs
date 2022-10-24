using UnityEngine; 
using System.Collections; 
using UnityEngine.EventSystems;

/*
 * 'ButtonPressed' is an helper script. 
 * By getting 'ispressed' by another script,
 * actions for each frame while the buttons is pressed
 * are possible.
 */

public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public bool ispressed = false;
	public void OnPointerDown(PointerEventData eventData)
	{
		ispressed = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		ispressed = false;
	}

} 