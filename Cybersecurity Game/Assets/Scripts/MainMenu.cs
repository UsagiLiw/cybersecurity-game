using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        SaveSystem.Init();
    }

    public void NewGame()
    {
        Debug.Log("Start new game");
        SaveObject saveObject =
            new SaveObject { day = 0, budget = 50, reputation = 30 };
        string json = JsonUtility.ToJson(saveObject, true);
        SaveSystem.Save (json);
        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        Debug.Log("Continue game");
        string saveString = SaveSystem.Load();
        if (saveString != null) SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
