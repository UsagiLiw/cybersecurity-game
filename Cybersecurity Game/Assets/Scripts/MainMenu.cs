using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("Start new game");
        //SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        Debug.Log("Continue game");
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
