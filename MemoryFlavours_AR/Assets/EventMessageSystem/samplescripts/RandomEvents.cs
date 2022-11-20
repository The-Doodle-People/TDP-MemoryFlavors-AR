using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * 'RandomEvents' calls an random event out of a list.
 * The list has to be predefined in the inspektor.
 * The execution of one of the events can be started 
 * by calling 'executeRandomEvent()' from a script or
 * by another event.
 */


public class RandomEvents : MonoBehaviour {
	[System.Serializable] public class mEvent : UnityEvent {}

	//List of possible events.
	public mEvent[] randomEvents;

	//Invoke of one of the predefined events.
	public void executeRandomEvent(){
		int index = Random.Range (0, randomEvents.Length);
		randomEvents [index].Invoke ();
	}
}
