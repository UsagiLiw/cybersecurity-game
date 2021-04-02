using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCcontroller : MonoBehaviour
{
    public static bool NPC_CEO;

    public static bool NPC_Employee1;

    public static bool NPC_Employee2;

    public static bool NPC_Meeting;

    public static bool NPC_IT;

    private void Start()
    {
        DisableAllNPC();
    }

    public static void TriggerNPCquest(Target target)
    {
        switch (target)
        {
            case Target.CEO:
                NPC_CEO = true;
                break;
            case Target.Employee1:
                NPC_Employee1 = true;
                break;
            case Target.Employee1:
                NPC_Employee2 = true;
                break;
            case Target.Meeting:
                NPC_Meeting = true;
                break;
            case Target.IT:
                NPC_IT = true;
                break;
            default:
                Debug.Log("Warning - target not an exisitng NPC");
        }
    }

    public static void DisableAllNPC()
    {
        NPC_CEO = false;
        NPC_Employee1 = false;
        NPC_Employee2 = false;
        NPC_Meeting = false;
        NPC_IT = false;
    }
}
