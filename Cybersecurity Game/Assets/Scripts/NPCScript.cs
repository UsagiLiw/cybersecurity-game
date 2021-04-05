using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (NPCcontroller.CheckTargetActive(self))
        {
            selfActive = true;
            QuestActive();
        }
    }

    private void Update()
    {
        StartCoroutine(DelayCheckStatus(2f));
        if (selfActive)
        {
            markerTempPos = markerIniPos;
            markerTempPos.y +=
                Mathf.Sin(Time.fixedTime * Mathf.PI * speed) * height;
            questMarker.transform.position = markerTempPos;
        }
    }

    private IEnumerator DelayCheckStatus(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (NPCcontroller.CheckTargetActive(self))
        {
            QuestActive();
        }
        else
        {
            QuestDeactive();
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
    }

    private void OnMouseDown()
    {
        if (selfActive)
        {
            Debug.Log("Go to quest");
            DisplayRequestScreen();
        }
    }

    private void DisplayRequestScreen()
    {
        GameObject requestScreen =
            Instantiate(requestScreen_Prefab) as GameObject;

        NPCRequest npcRequest = requestScreen.GetComponent<NPCRequest>();
        npcRequest.EnableNPC(self);
        // Text dialog =
        //     requestScreen
        //         .transform
        //         .GetChild(0)
        //         .transform
        //         .GetChild(0)
        //         .gameObject
        //         .GetComponent<Text>();
        // dialog.text = "Im a man";
    }
}
