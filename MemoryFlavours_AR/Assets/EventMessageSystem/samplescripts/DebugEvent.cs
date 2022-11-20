using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 'DebugEvent' is an helper script for debugging unity event invokes.
 * By calling 'debug()' by an event, 
 * debug messages without scripting are generated.
 * 
 * 'debugBreak()' will pause the game execution in the editor.
 */

public class DebugEvent : MonoBehaviour {

	public void debug(string text){
		Debug.Log (text);
	}

	public void debugBreak(){
		Debug.Break ();
	}
}
