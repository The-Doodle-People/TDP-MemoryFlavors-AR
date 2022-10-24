using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * 'OnCollisionTag' invokes an unity event,
 * if an collision on this gameobject with another 
 * collider with the configurable tag
 * occures. The rules regarding colliders, rigidbodies and
 * collision layers also apply here.
 */

public class OnCollisionTag : MonoBehaviour {

	public string Tag = "Player";
	public bool isEnabled = true;
	bool debug = false;

	[System.Serializable] public class mEvent : UnityEvent {}
	public mEvent _OnCollisionEnter;

    void OnCollisionEnter(Collision collision)    {
		if (debug == true) {
			Debug.Log ("Debug");
		}
             if (collision.gameObject.tag == Tag && isEnabled == true) {
             _OnCollisionEnter.Invoke ();
		}
	}
}

