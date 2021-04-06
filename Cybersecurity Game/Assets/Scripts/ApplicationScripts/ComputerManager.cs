using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    // index 0 : Player's computer
    // index 1 - 4 : NPCs' computer

    [SerializeField]
    private Computer[] computers = new Computer[5];

    public static Computer activeComputer;

    public Computer GetComputer(int index)
    {
        return computers[index];
    }

    /*
     * index range from 0 - 5 to indicate which computer is active
     */
    public void SetActiveComputer(int index)
    {
        activeComputer = computers[index];
    }

    public void ActivateAntivirus()
    {
        foreach(Computer computer in computers)
        {
            computer.haveAntivirus = true;
        }
    }
}

[Serializable]
public class Computer
{
    [Range(0, 1)] public float ram;
    [Range(0, 1)] public float cpu;
    [Range(0, 1)] public float disk;
    [Range(0, 200)] public float driveC;
    [Range(0, 400)] public float driveD;
    [Range(0, 400)] public float driveE;
    public List<int> software;
    public List<int> malware;
    public bool haveAntivirus;
    public bool isInfected;
}