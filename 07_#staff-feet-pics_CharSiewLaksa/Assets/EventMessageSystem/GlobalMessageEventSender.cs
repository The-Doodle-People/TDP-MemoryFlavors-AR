using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 'GlobalMessageEventSender' is usually called by an unity event.
 * It transmitts a message to all 'GlobalMessageEventReceiver' scripts which are
 * registered at the 'GlobalMessageEventManager' (creation of the manager and
 * registering of the receivers happens automatically, if at least one receiver is or was 
 * within the scene).
 * 
 * The messages, which will trigger unity events, can be transmitted over
 * multible loaded scenes and will also reach spawned gameobjects (the gameobjects must 
 * be activated). 
 * 
 * To send a message within a script, use 'GlobalMessageEventManager.sendToReceivers ("yourMessage");'.
 */

public class GlobalMessageEventSender : MonoBehaviour {

	public void GlobalMessage( string trigger) {

		if (GlobalMessageEventManager.instance != null) {
																	//If the manager is correctly initialized..
			GlobalMessageEventManager.sendToReceivers (trigger);	//..let him delegate the message to all the registered receivers.

		} else {

			//Alternatively search for all gameobjects with an receiver and give them the message.
			//This will not transmit messages over multible scenes and has an awful performance.
			//It should not be possible to enter this path, because the manager should be automatically present,
			//if one receiver is or was in this or an previous scene (except something destroyed it).
			Debug.LogWarning("'GlobalMessageEventManager' is missing. This shouldn't happen, if one receiver was or is within the game.");

			GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType (typeof(GameObject));
			foreach (GameObject go in gos) {
				if (go && go.transform.parent == null) {
					GlobalMessageEventReceiver[] rx = go.GetComponentsInChildren<GlobalMessageEventReceiver> ();
					if (rx != null) {
						if (rx.Length > 0) {
							foreach (GlobalMessageEventReceiver r in rx) {
								r.globalMessage (trigger);
							}
						}
					}
				}
			}
		}
	}
}
