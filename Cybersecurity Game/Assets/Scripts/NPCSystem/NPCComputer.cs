using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComputer : MonoBehaviour
{
    public GameObject screen1;

    public GameObject screen2;

    public GameObject screen3;

    public GameObject screen4;

    public GameObject screen5;

    public GameObject screen6; //not actually use

    private GameObject UIPanel;

    void OnEnable()
    {
        UIPanel = GameObject.FindGameObjectWithTag("UIPanel");
        UIPanel.SetActive(false);
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        UIPanel.SetActive(true);
    }
}
