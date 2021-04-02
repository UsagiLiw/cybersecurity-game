using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public Target self;

    // public GameObject NPC;
    public GameObject questMarker;

    public GameObject NPCmodel;

    private Outline outline;

    private void Start()
    {
        questMarker.SetActive(false);
        outline = NPCmodel.GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnEnable()
    {
        Debug.Log (self);
        if (NPCcontroller.CheckTargetActive(self))
        {
            QuestActive();
        }
    }

    private void OnMouseOver()
    {
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }

    private void QuestActive()
    {
        questMarker.SetActive(true);
    }

    private void QuestDeactive()
    {
        questMarker.SetActive(false);
    }
}
