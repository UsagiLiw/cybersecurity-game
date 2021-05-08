using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject continue_Button;

    private void Awake()
    {
        SaveSystem.Init();
    }

    private void Start()
    {
        string saveString = SaveSystem.Load();
        if (saveString == null) continue_Button.SetActive(false);
    }

    public void NewGame()
    {
        SaveObject saveObject =
            new SaveObject { day = 0, budget = 50, reputation = 30 };
        string json = JsonUtility.ToJson(saveObject, true);
        SaveSystem.Save (json);
        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null) SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
