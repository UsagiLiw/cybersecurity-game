using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCcontroller : MonoBehaviour
{
    private static bool NPC_CEO;

    private static bool NPC_Employee1;

    private static bool NPC_Employee2;

    private static bool NPC_Meeting;

    private static bool NPC_IT;

    private static Scenario scenario;

    private void Start()
    {
        DisableAllNPC();
    }

    public static void TriggerNPCquest(Target target, Scenario type)
    {
        scenario = type;
        switch (target)
        {
            case Target.CEO:
                NPC_CEO = true;
                break;
            case Target.Employee1:
                NPC_Employee1 = true;
                break;
            case Target.Employee2:
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
                break;
        }
    }

    public static bool CheckTargetActive(Target target)
    {
        switch (target)
        {
            case Target.CEO:
                if (NPC_CEO)
                {
                    return true;
                }
                break;
            case Target.Employee1:
                if (NPC_Employee1)
                {
                    return true;
                }
                break;
            case Target.Employee2:
                if (NPC_Employee2)
                {
                    return true;
                }
                break;
            case Target.Meeting:
                if (NPC_Meeting)
                {
                    return true;
                }
                break;
            case Target.IT:
                if (NPC_IT)
                {
                    return true;
                }
                break;
            default:
                return false;
        }
        return false;
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
