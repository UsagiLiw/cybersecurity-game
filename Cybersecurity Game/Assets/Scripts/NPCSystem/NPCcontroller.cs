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

    private static Target currentTarget;

    private static string requestDetail;

    // public static bool isPhishing;
    public delegate void NewNPCScenarioAction();

    public static event NewNPCScenarioAction NewNPCScenario;

    //Scenario Computer screen prefab
    public GameObject phishingScreen_prefab;

    public ComputerUI computerUI;
    // public GameObject NPCcomputer_prefab;

    private void Awake()
    {
        DisableAllNPC();
    }

    public static void TriggerNPCQuest(
        Target target,
        string detail,
        Scenario currentScenario
    )
    {
        scenario = currentScenario;
        currentTarget = target;
        requestDetail = detail;
        TriggerNPC (target);
    }

    // public static void TriggerNPCPhishingQuest(
    //     Target target,
    //     string detail,
    //     bool isAPhishing
    // )
    // {
    //     scenario = Scenario.Phishing;
    //     currentTarget = target;
    //     requestDetail = detail;
    //     // isPhishing = isAPhishing;
    //     TriggerNPC (target);
    // }
    // public static void TriggerNPCMalwareQuest(Target target, string detail)
    // {
    //     scenario = Scenario.Malware;
    //     currentTarget = target;
    //     requestDetail = detail;
    //     TriggerNPC (target);
    // }
    private static void TriggerNPC(Target target)
    {
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
        if (NewNPCScenario != null)
        {
            NewNPCScenario.Invoke();
        }
    }

    public static void ContinueNPCquest()
    {
        if (NewNPCScenario != null)
        {
            NewNPCScenario.Invoke();
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

        scenario = 0;
        currentTarget = 0;
        requestDetail = null;
    }

    public void CreateNPCScreen()
    {
        switch (scenario)
        {
            case Scenario.Phishing:
                CreatePhishingScreen();
                break;
            case Scenario.Malware:
                CreateMalwareScreen();
                break;
            default:
                Debug.Log("NPC screen type unspecify: " + scenario);
                break;
        }
    }

    private void CreatePhishingScreen()
    {
        GameObject phishingScreen_Object =
            Instantiate(phishingScreen_prefab) as GameObject;
        GameObject gui = GameObject.Find("GUI");
        phishingScreen_Object.transform.SetParent(gui.transform, false);
    }

    private void CreateMalwareScreen()
    {
        Debug.Log("Start malware screen");
        computerUI.StartComputer((int) currentTarget);
    }

    public static (Scenario, Target, string) GetRequestDetail()
    {
        return (scenario, currentTarget, requestDetail);
    }

    public static Target GetRequestTarget()
    {
        return currentTarget;
    }
}
