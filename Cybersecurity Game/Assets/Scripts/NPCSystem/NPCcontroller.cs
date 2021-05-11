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

    //Notification String
    public static string ceo_quote = "I require your attention.";

    public static string section1_quote = "I need your help!";

    public static string section2_quote = "Can you come take a look?";

    public static string
        meeting_quote = "Please reporting in as soon as possible.";

    public static string it_quote = "Hey, I need your feedback.";

    //Announcing quest change
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

    private static void TriggerNPC(Target target)
    {
        switch (target)
        {
            case Target.CEO:
                NPC_CEO = true;
                NotificationManager
                    .SetNewNotification(new Notification("CEO Room",
                        ceo_quote));
                break;
            case Target.Employee1:
                NPC_Employee1 = true;
                NotificationManager
                    .SetNewNotification(new Notification("Section1",
                        section1_quote));
                break;
            case Target.Employee2:
                NPC_Employee2 = true;
                NotificationManager
                    .SetNewNotification(new Notification("Section2",
                        section2_quote));
                break;
            case Target.Meeting:
                NPC_Meeting = true;
                NotificationManager
                    .SetNewNotification(new Notification("Meeting Room",
                        meeting_quote));
                break;
            case Target.IT:
                NPC_IT = true;
                NotificationManager
                    .SetNewNotification(new Notification("IT Room", it_quote));
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

        ContinueNPCquest();
    }

    public void CreateNPCScreen()
    {
        switch (scenario)
        {
            case Scenario.Phishing:
                CreatePhishingScreen();
                break;
            case Scenario.Malware:
                computerUI.StartComputer((int) currentTarget);
                break;
            case Scenario.Ransom:
                computerUI.StartComputer((int) currentTarget);
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

    public static (Scenario, Target, string) GetRequestDetail()
    {
        return (scenario, currentTarget, requestDetail);
    }

    public static Target GetRequestTarget()
    {
        return currentTarget;
    }
}
