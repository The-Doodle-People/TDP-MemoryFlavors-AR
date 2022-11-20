using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * 'OnEnterTag' invokes an unity event,
 * if collider enter on this gameobject with another 
 * collider with the configurable tag
 * occures. The rules regarding colliders, rigidbodies and
 * collision layers also apply here.
 */


public class OnEnterTag : MonoBehaviour {

	public string Tag = "Player";
	bool isEnabled = true;

	[System.Serializable] public class mEvent : UnityEvent {}
	public mEvent _OnTriggerEnter;

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == Tag && isEnabled == true) {

			_OnTriggerEnter.Invoke ();
		}
	}
}
