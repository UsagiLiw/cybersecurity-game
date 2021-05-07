using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; private set; }

    public GameObject timeBar;

    public GameObject uiPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void SetActiveStatus(bool time, bool panel)
    {
        timeBar.SetActive (time);
        uiPanel.SetActive (panel);
    }
}
