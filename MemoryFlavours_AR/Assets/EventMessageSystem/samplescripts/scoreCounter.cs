using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class scoreCounter : MonoBehaviour {

	 public int score;

	public string key = "myScore";


	public void increase(int inc){
//		Debug.Log ("add score: " + inc.ToString ());
		load ();
		score += inc;
		save ();
	}

	public void setScore(int newScore){
		score = newScore;
//		Debug.Log ("new score: " + newScore.ToString () + " for '"+ key + "'");
		save ();
	}

	public int getScore(){
		load ();
		return score;
	}

	public void setMaxScore(int newScore){
		load ();
		if (newScore > score) {
			score = newScore;
			//Debug.Log ("new high Score: " + score.ToString ());
		}
		save ();
	}

	void save(){
		PlayerPrefs.SetInt (key, score);
	}

	int load(){
		score = PlayerPrefs.GetInt (key);
		return score;
	}


	// Use this for initialization
	void Start () {
		load();
	}

	public Text scoreValue;

	// Update is called once per frame
	void Update () {
		if (scoreValue != null) {
			scoreValue.text = score.ToString ();
		}
	}

}
