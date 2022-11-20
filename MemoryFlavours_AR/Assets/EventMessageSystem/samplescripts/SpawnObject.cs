using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

	public GameObject[] prefab;
	public Transform parent;
	public GameObject actualSpawn;


	public List<GameObject> allSpawns;

	public void destroyActualSpawn (){
		allSpawns.Remove (actualSpawn);
		Destroy (actualSpawn);
	}

	public void destroyAndSpawn(){
		destroyActualSpawn ();
		int rnd = Random.Range (0, prefab.Length);
		actualSpawn = (GameObject)Instantiate (prefab[rnd],parent);
		allSpawns.Add (actualSpawn);
	}

	public void spawnAdditive(){
		int rnd = Random.Range (0, prefab.Length);
		actualSpawn = (GameObject)Instantiate (prefab[rnd],parent);
		allSpawns.Add (actualSpawn);
	}

	public void destroyAllSpawns(){
		foreach (GameObject go in allSpawns) {
			if (go != null) {
				Destroy (go);
			}
		}
		allSpawns.Clear ();
	}
}
