using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	public void menuButton()
	{
		SceneManager.LoadScene(19);
		Debug.Log("Changing to MainMenu Scene");
	}

	public void gameButton()
	{
		SceneManager.LoadScene(20);
		Debug.Log("Changing to Game Menu Scene");
	}

	public void sidegameButton()
	{
		SceneManager.LoadScene(1);
		Debug.Log("Changing to Side Game Scene");
	}

	public void maingameButton()
	{
		SceneManager.LoadScene(21);
		Debug.Log("Changing to Main Game Scene");
	}
}
