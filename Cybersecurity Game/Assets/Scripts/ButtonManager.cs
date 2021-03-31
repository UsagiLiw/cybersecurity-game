using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private static ButtonManager instance;
    public GameObject computerUI;
    private GameObject gameUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        gameUI = GameObject.Find("UIPanel");

    }

    public void ButtonMoveScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void ButtonComputer(string pc)
    {
        computerUI.SetActive(true);
        gameUI.SetActive(false);
    }
}
