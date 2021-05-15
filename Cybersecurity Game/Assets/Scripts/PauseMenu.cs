using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject gameUI;

    public Text shopPassword;

    public Text emailPassword;

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
    }

    void Pause()
    {
        shopPassword.text = PasswordManager.password1;
        emailPassword.text = PasswordManager.password2;
        pauseMenuUI.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void QuitGame()
    {
        GameManager.BackToMainMenu(true);
        Destroy(this.gameObject);
        Time.timeScale = 1f;
    }
}
