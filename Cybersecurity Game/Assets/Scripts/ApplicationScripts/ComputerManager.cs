using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager Instance { get; private set; }

    // index 0 : Player's computer
    // index 1 - 5 : NPCs' computer
    [SerializeField]
    private Computer[] computers = new Computer[6];

    public static Computer activeComputer;

    public static bool haveAntivirus;

    //Announcing change in activeCom
    public delegate void ActiveComAction();

    public static event ActiveComAction NewActiveComAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy (gameObject);
        }

        for (int i = 0; i < computers.Length; i++)
        {
            computers[i].ram = Random.Range(0.3f, 0.7f);
            computers[i].cpu = Random.Range(0.2f, 0.7f);
            computers[i].disk = Random.Range(0.3f, 0.7f);
            computers[i].driveC = Random.Range(30, 199);
            computers[i].driveD = Random.Range(80, 399);
            computers[i].driveE = Random.Range(80, 399);
            computers[i].isInfected = false;
            computers[i].isSlow = false;
            computers[i].isBuggy = false;
        }
    }

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

    public void DeactivateAntivirus()
    {
        haveAntivirus = false;
    }

    public void AddMalwareToCom(Target target, int malwareIndex)
    {
        computers[(int) target].malware.Add(malwareIndex);
        computers[(int) target].isInfected = true;
        ChangeOnActiveCom (target);
    }

    public void PurgeMalwareOnCom(Target target)
    {
        computers[(int) target].malware.Clear();
        computers[(int) target].isInfected = false;
        computers[(int) target].isSlow = false;
        computers[(int) target].isBuggy = false;
        ChangeOnActiveCom (target);
    }

    public static void PurgeMalwareOnActiveCom()
    {
        activeComputer.malware.Clear();
        activeComputer.isInfected = false;
        activeComputer.isSlow = false;
        activeComputer.isBuggy = false;
        if (NewActiveComAction != null) NewActiveComAction.Invoke();
    }

    public void DiskOverload(Target target)
    {
        int index = (int) target;
        computers[index].driveC = 199;
        computers[index].driveD = 398;
        computers[index].driveE = 399;
        ChangeOnActiveCom (target);
    }

    public void SystemOverload(Target target)
    {
        int index = (int) target;
        computers[index].ram = 0.89f;
        computers[index].cpu = 0.98f;
        computers[index].disk = 0.99f;
        ChangeOnActiveCom (target);
    }

    public void SetComputerBehavior(Target target, bool slow, bool buggy)
    {
        computers[(int) target].isSlow = slow;
        computers[(int) target].isBuggy = buggy;
        ChangeOnActiveCom (target);
    }

    public int CheckActiveComMalwareIndex()
    {
        if (activeComputer.isInfected == true)
        {
            return activeComputer.malware[0];
        }
        return -1;
    }

    public int CheckActiveComMalwareType()
    {
        if (activeComputer.isInfected == true)
        {
            MalwareType malware =
                MalwareManager
                    .GetMalwareTypeFromIndex(activeComputer.malware[0]);
            return (int) malware;
        }
        return -1;
    }

    private void ChangeOnActiveCom(Target target)
    {
        if (activeComputer == null) return;

        if ((int) target == (int) activeComputer.owner)
        {
            activeComputer = computers[(int) target];
            if (NewActiveComAction != null) NewActiveComAction.Invoke();
        }
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

    public bool isBuggy;
}
