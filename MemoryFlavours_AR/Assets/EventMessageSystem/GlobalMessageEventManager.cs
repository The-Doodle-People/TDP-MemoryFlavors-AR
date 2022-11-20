using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * GlobalMessageEventManager collects receivers for string messages 
 * (like triggers of an animator) and distributes the messages to them.
 * Depending on the message, the receiver calls an unity event.
 * 
 * The Manger is automatically created as an "DontDestroyOnLoad" - gameobject,
 * if any receiver is or was in scene. You should NOT add it manually to an gameobject. 
 * Because of this structure, messages can be transmitted over multible opened scenes.
 * 
 */

public class GlobalMessageEventManager : MonoBehaviour {


	public static GlobalMessageEventManager instance;

	public List<GlobalMessageEventReceiver> receivers;

	void Awake(){
		buildAwake ();
	}

	//If no manager exists yet, mark the gameobject as 'DontDestroyOnLoad' and create receiver-list,
	//else self destruct.
	public void buildAwake(){
		if (instance == null) {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
			receivers = new List<GlobalMessageEventReceiver> ();
		} else {
			if (instance != this) {
				Destroy (gameObject);
			}
		}
	}

	//called by receivers: register for message receiving
	public static void registerMessageReceiver(GlobalMessageEventReceiver recv){
		if (instance != null) {
			instance.receivers.Add (recv);
		}
	}

	//called by receivers (in OnDestro()) : unregister for message receiving
	public static void unregisterMessageReceiver(GlobalMessageEventReceiver recv){
		if (instance != null) {
			instance.receivers.Remove (recv);
		}
	}

	//called by transmitter script: send message to all registered receivers
	public static void sendToReceivers(string message){
		if (instance != null) {
			//Debug.Log ("Manager, sending:'" + message + "'");
			foreach (GlobalMessageEventReceiver recv in instance.receivers) {
				recv.globalMessage (message);
			}
		} else {
			Debug.Log ("Warning: Use GlobalMessageEventManager missing. The creation should happen automatically if a receiver is present.");
		}
	}

}
