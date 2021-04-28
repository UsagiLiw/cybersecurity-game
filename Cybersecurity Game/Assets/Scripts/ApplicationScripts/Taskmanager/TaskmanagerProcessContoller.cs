using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskmanagerProcessContoller : MonoBehaviour
{
    public ProcessClass[] essential;

    public ProcessClass[] addOn;

    public ProcessClass[] anomaly;

    private void OnEnable()
    {
        Debug.Log("Under contruction");
    }
}

[System.Serializable]
public class ProcessClass
{
    public string name;

    public int cpu;

    public int memory;

    public int disk;

    public int network;
}
