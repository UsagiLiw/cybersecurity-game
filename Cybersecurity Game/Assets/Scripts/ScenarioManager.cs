using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance { get; private set; }

    private PwdAtkController pwdAtkController;

    private PhishingController phishingController;

    private MalwareController malwareController;

    public ScenarioClass[] scenarioTypes;

    public static Scenario onGoingScenario;

    public static bool underAttack;

    public static string jsonDetail;

    //Delegate method to announce the end of scenario
    public delegate void ScenarioAction();

    public static event ScenarioAction ScenarioEnded;

    // public static string saveObject;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject); // Don't kil me
        }
        else
        {
            Destroy (gameObject);
        }

        pwdAtkController = GetComponent<PwdAtkController>();
        phishingController = GetComponent<PhishingController>();
        malwareController = GetComponent<MalwareController>();
    }

    public (Scenario, string) CheckStatus()
    {
        Scenario index = 0;
        if (!underAttack)
        {
            index = ScenarioRandomizer();
            underAttack = true;
            onGoingScenario = index;
            return TriggerScenario(index);
        }
        else
        {
            return UpdateScenarioStatus();
        }
    }

    public void SetScenarioState(Scenario currentScenario, string detail)
    {
        if (currentScenario != Scenario.None && String.IsNullOrEmpty(detail))
        {
            Debug.Log("Warning, On going scenario but detail is empty");
            onGoingScenario = Scenario.None;
            underAttack = false;
        }
        else
        {
            onGoingScenario = currentScenario;
        }
        switch (onGoingScenario)
        {
            case Scenario.None:
                underAttack = false;
                break;
            case Scenario.Password:
                underAttack = true;
                pwdAtkController.SetPasswordScenarioState (detail);
                break;
            case Scenario.Phishing:
                underAttack = true;
                phishingController.SetPhishingScenarioState (detail);
                break;
            case Scenario.Malware:
                underAttack = true;
                malwareController.SetMalwareScenarioState (detail);
                break;
            default:
                throw new InvalidOperationException("Error: Unknown scenario index: " +
                    onGoingScenario);
        }
    }

    private (Scenario, string) UpdateScenarioStatus()
    {
        string detail = null;
        Scenario currentType;
        switch (onGoingScenario)
        {
            case Scenario.None:
                underAttack = false;
                currentType = Scenario.None;
                break;
            case Scenario.Password:
                (underAttack, detail) = pwdAtkController.UpdateScenarioState();
                currentType = Scenario.Password;
                break;
            case Scenario.Phishing:
                (underAttack, detail) =
                    phishingController.UpdateScenarioState();
                currentType = Scenario.Phishing;
                break;
            case Scenario.Malware:
                (underAttack, detail) = malwareController.UpdateScenarioState();
                currentType = Scenario.Malware;
                break;
            default:
                throw new InvalidOperationException("Error: Unknown scenario index: " +
                    onGoingScenario);
        }

        // true = scenario still on and continue, false = player failed the scenario
        if (underAttack)
        {
            jsonDetail = detail;
            return (currentType, detail);
        }
        else
        {
            jsonDetail = detail;
            InvokeScenarioFailed (detail);
            return (Scenario.None, null);
        }
    }

    private Scenario ScenarioRandomizer()
    {
        int i = Random.Range(0, 100);
        Debug.Log("Sc random result: " + i);
        for (int j = 0; j < scenarioTypes.Length; j++)
        {
            if (
                i >= scenarioTypes[j].minProbRange &&
                i <= scenarioTypes[j].maxProbRange
            )
            {
                Debug
                    .Log("Scenario " +
                    scenarioTypes[j].scenarioType +
                    " has occur!");
                return scenarioTypes[j].scenario;
            }
        }
        return 0;
        // throw new InvalidOperationException("Error in ScenarioRandomizer, random process failed");
    }

    private (Scenario, string) TriggerScenario(Scenario chosenScenario)
    {
        int i = 0;
        underAttack = true;
        switch (chosenScenario)
        {
            case Scenario.None:
                Debug
                    .Log("Warning: Trigger scenario.None - Should not enter this case at all");
                return (Scenario.None, "");
            case Scenario.Password:
                jsonDetail = pwdAtkController.CheckVulnerability();
                return (Scenario.Password, jsonDetail);
            case Scenario.Phishing:
                i = TargetRandomizer(true);

                //Self phishing does not need following
                if (i == 0)
                {
                    phishingController.TriggerSelf();
                    underAttack = false;
                    return (Scenario.None, "");
                }
                return (Scenario.Phishing, phishingController.TriggerNPC(i));
            case Scenario.Malware:
                i = TargetRandomizer(false);
                return (Scenario.Malware, malwareController.TriggerNPC(i));
            default:
                throw new InvalidOperationException("Error: Unknown scenario index: " +
                    chosenScenario);
        }
    }

    private int TargetRandomizer(bool self)
    {
        int targetCount = System.Enum.GetValues(typeof (Target)).Length - 1;
        int i = 0;
        if (!self)
        {
            i = 1;
        }
        return Random.Range(i, targetCount);
    }

    public static void InvokeScenarioSuccess(string result)
    {
        NPCcontroller.DisableAllNPC();
        onGoingScenario = Scenario.None;
        underAttack = false;
        EmailManager.ClearScenarioMails();
        ResultController.ShowSuccess (result, onGoingScenario);
        jsonDetail = null;
        InvokeScenarioEnded();
        GameManager.InvokeSaveData();
    }

    public static void InvokeScenarioFailed(string result)
    {
        NPCcontroller.DisableAllNPC();
        ResultController.ShowFailed (result, onGoingScenario);
        onGoingScenario = Scenario.None;
        underAttack = false;
        EmailManager.ClearScenarioMails();
        jsonDetail = null;
        InvokeScenarioEnded();
        GameManager.InvokeSaveData();
    }

    public static void InvokeScenarioFailed(string result, Scenario currentSce)
    {
        NPCcontroller.DisableAllNPC();
        ResultController.ShowFailed (result, currentSce);
        onGoingScenario = Scenario.None;
        underAttack = false;
        EmailManager.ClearScenarioMails();
        jsonDetail = null;
        InvokeScenarioEnded();
        GameManager.InvokeSaveData();
    }

    private static void InvokeScenarioEnded()
    {
        if (ScenarioEnded != null)
        {
            ScenarioEnded.Invoke();
        }
    }
}

[System.Serializable]
public class ScenarioClass
{
    public string scenarioType;

    public Scenario scenario;

    public int minProbRange = 0;

    public int maxProbRange = 0;
}
