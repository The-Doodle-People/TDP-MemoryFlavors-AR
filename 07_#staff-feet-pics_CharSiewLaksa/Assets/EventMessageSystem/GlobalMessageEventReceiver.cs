using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * The 'GlobalMessageEventReceiver' is called by the 'GlobalMessageEventManager'.
 * If no manager exist, it is created by this script.
 * 
 * You have to add this receiver to gameobjects, which shall be affected by a 
 * particular message. Depending on this adjustable string messages, this receiver will execute a 
 * corresponding unity event. 
 * 
 * The receiver can be added to prefabs and is ready after the activation of the gameobject.
 */

public class GlobalMessageEventReceiver : MonoBehaviour {

	[System.Serializable] public class mEvent : UnityEvent {}

	bool OutputReceivedMessages = false;

	[System.Serializable]
	public class MessageEvent
	{
		public string trigger;
		public mEvent _event;
	}	
		
	void Start(){
		createManagerIfNonExisting ();
		//register the receiver at the manager, to get the delegation of messages
		GlobalMessageEventManager.registerMessageReceiver (this);
	}

	//create a message manager, if it isn't existing yet
	void createManagerIfNonExisting(){
		if (GlobalMessageEventManager.instance == null) {
			GameObject go = new GameObject("GlobalMessageEventManager");
			go.AddComponent <GlobalMessageEventManager>();
			GlobalMessageEventManager gem = go.GetComponent <GlobalMessageEventManager> ();
			gem.buildAwake ();
		}
	}

	//unregister the receiver at the manager, to stop the delegation of messages
	void OnDestroy(){
		GlobalMessageEventManager.unregisterMessageReceiver (this);
	}

	[Tooltip("List of event - message combinations. Only relevant messages for this gameobject have to be added.")]
	public MessageEvent[] MessageEvents;


	/*
	 * 'globalMessage()' is usually called by the management script to delegate the message. 
	 */

	public void globalMessage(string trigger){

		if (OutputReceivedMessages == true) {
			Debug.Log (trigger);
		}

		int invokeCnt = 0;
		foreach (MessageEvent me in MessageEvents) {
			if (trigger == me.trigger) {	//if the message was configured..
				me._event.Invoke ();		//..execute/invoke the corresponding event
				invokeCnt++;
			}
		}
		if (invokeCnt == 0) {
			//Zero executions are possible, because of multible receivers with different triggers.
			//This time no message/trigger for this receiver was sent.
		}
	}
}
