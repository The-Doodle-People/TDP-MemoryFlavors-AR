using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        Text.text = score.ToString() + "Score";
    }
  
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(2);
    }
}
