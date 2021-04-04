using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
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

        Debug.Log("this gameobject " + name);
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
        Debug.Log("Quest Active");
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
        }
    }
}
