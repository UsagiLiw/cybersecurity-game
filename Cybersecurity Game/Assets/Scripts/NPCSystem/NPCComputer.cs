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

    private Target currentTarget;

    private ComputerManager computerManager;

    //Currently active computer
    private GameObject currentCom;

    private GameObject taskbar;

    private GameObject software1;

    private GameObject software2;

    //Application

    // public GameObject browser;
    // public GameObject 

    void OnEnable()
    {
        UIPanel = GameObject.FindGameObjectWithTag("UIPanel");
        UIPanel.SetActive(false);
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        currentTarget = NPCcontroller.GetRequestTarget();
        GetComputerManager();
        currentCom =
            this.transform.GetChild((int) currentTarget - 1).gameObject;
        currentCom.SetActive(true);
        taskbar = currentCom.transform.GetChild(0).gameObject;
        software1 = currentCom.transform.GetChild(1).gameObject;
        software2 = currentCom.transform.GetChild(2).gameObject;

        if (ComputerManager.haveAntivirus)
            software1.transform.Find("Antivirus").gameObject.SetActive(true);
        else
            software1.transform.Find("Antivirus").gameObject.SetActive(false);
    }

    void OnDisable()
    {
        UIPanel.SetActive(true);
    }

    private void GetComputerManager()
    {
        computerManager =
            GameObject
                .FindGameObjectWithTag("GameManager")
                .transform
                .GetComponent<ComputerManager>();
    }
}
