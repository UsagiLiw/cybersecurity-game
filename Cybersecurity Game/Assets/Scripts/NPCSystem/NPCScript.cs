using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public GameObject requestScreen_Prefab;

    private NPCScript thisNPC;

    public Target self;

    private bool selfActive;

    public GameObject questMarker;

    public GameObject NPCmodel;

    private Outline outline;

    private float height = 0.3f;

    private float speed = 1f;

    private Vector3 markerIniPos = new Vector3();

    private Vector3 markerTempPos = new Vector3();

    private void OnEnable()
    {
        outline = NPCmodel.GetComponent<Outline>();
        outline.enabled = false;
        markerIniPos = questMarker.transform.position;
        QuestDeactive();
        NPCcontroller.NewNPCScenario += CheckSelfIsActive;
        CheckSelfIsActive();
    }

    private void OnDisable()
    {
        NPCcontroller.NewNPCScenario -= CheckSelfIsActive;
    }

    private void Update()
    {
        // StartCoroutine(DelayCheckStatus(2f));
        if (selfActive)
        {
            markerTempPos = markerIniPos;
            markerTempPos.y +=
                Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * height;
            questMarker.transform.position = markerTempPos;
        }
    }

    private void CheckSelfIsActive()
    {
        if (NPCcontroller.CheckTargetActive(self))
        {
            selfActive = true;
            QuestActive();
        }
    }
    
    private void OnMouseOver()
    {
        if (selfActive) outline.enabled = true;
    }

    private void OnMouseExit()
    {
        if (selfActive) outline.enabled = false;
    }

    private void QuestActive()
    {
        questMarker.SetActive(true);
        selfActive = true;
    }

    private void QuestDeactive()
    {
        questMarker.SetActive(false);
        selfActive = false;
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        if (selfActive)
        {
            QuestDeactive();
            DisplayRequestScreen();
        }
    }

    private void DisplayRequestScreen()
    {
        GameObject requestScreen =
            Instantiate(requestScreen_Prefab) as GameObject;

        NPCRequest npcRequest = requestScreen.GetComponent<NPCRequest>();
        npcRequest.EnableNPC();
        QuestDeactive();
    }
}
