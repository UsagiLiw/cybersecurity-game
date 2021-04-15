using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    // index 0 : Player's computer
    // index 1 - 4 : NPCs' computer
    [SerializeField]
    private Computer[] computers = new Computer[6];

    public static Computer activeComputer;

    public static bool haveAntivirus;

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
        haveAntivirus = true;
    }

    public void AddMalwareToCom(Target target, int malwareIndex)
    {
        computers[(int) target].malware.Add(malwareIndex);
    }

    public void CleanMalwareOnCom(Target target)
    {
        computers[(int) target].malware.Clear();
    }

    public void DiskOverload(Target target)
    {
        computers[(int) target].driveC = 199;
        computers[(int) target].driveD = 398;
        computers[(int) target].driveE = 399;
    }

}

[Serializable]
public class Computer
{
    public Target owner;

    [Range(0, 1)]
    public float ram;

    [Range(0, 1)]
    public float cpu;

    [Range(0, 1)]
    public float disk;

    [Range(0, 200)]
    public float driveC;

    [Range(0, 400)]
    public float driveD;

    [Range(0, 400)]
    public float driveE;

    // public List<int> software;
    public List<int> malware;

    public bool isInfected;
}
