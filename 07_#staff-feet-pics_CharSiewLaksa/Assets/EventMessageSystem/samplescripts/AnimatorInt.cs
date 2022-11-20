using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 'AnimatorInt' is an helper script.
 * By calling it by an unity event, it can modify a
 * int value of an animator without scripting.
 * This is also useful in combination with the 'GlobalMessageEventReceiver'.
 */


public class AnimatorInt : MonoBehaviour {

	public Animator targetAnimator;
	public string IntName = "int";

	public void setInt(int value){
		targetAnimator.SetInteger (IntName, value);
	}
}
