using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    // index 0 : Player's computer
    // index 1 - 5 : NPCs' computer
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

    public bool ActiveComIsSlow()
    {
        
        if (activeComputer.isSlow)
        {
            return true;
        }
        return false;
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
        int index = (int) target;
        computers[index].driveC = 199;
        computers[index].driveD = 398;
        computers[index].driveE = 399;
    }

    public void SystemOverload(Target target)
    {
        int index = (int) target;
        computers[index].ram = 0.89f;
        computers[index].cpu = 0.98f;
        computers[index].disk = 0.99f;
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

    public bool isSlow;
}
