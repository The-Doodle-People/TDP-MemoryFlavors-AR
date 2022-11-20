using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 'AnimatorBool' is an helper script.
 * By calling it by an unity event, it can modify a
 * bool value of an animator without scripting.
 * This is also useful in combination with the 'GlobalMessageEventReceiver'.
 */

public class AnimatorBool : MonoBehaviour {

	public Animator targetAnimator;
	public string BoolName = "bool";

	public void setBool(bool value){
		targetAnimator.SetBool (BoolName, value);
	}
}

