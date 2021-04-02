using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public Target self;

    // public GameObject NPC;

    public GameObject questMarker;

    private Outline outline;

    private void Start()
    {
        questMarker.SetActive(false);
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnEnable()
    {
        Debug.Log (self);
        if (NPCcontroller.CheckTargetActive(self))
        {
            Debug.Log("WOW");
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("enable");
        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("exit");
        outline.enabled = false;
    }
}
