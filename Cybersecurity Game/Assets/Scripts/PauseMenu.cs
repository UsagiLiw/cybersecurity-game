using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject gameUI;

    // Update is called once per frame
    public void GamePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Game Resume");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Game Pause");
    }

    void LoadOptions()
    {
        Debug.Log("Load Option Menu.....");
    }

    void QuitGame()
    {
        Debug.Log("Quitting Game......");
    }
}
