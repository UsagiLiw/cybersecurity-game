using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ScenarioManager : MonoBehaviour
{
    static ScenarioManager SingletonScenarioManager;

    private PwdAtkController pwdAtkController;

    private PhishingController phishingController;

    public ScenarioClass[] scenarioTypes;

    public static Scenario onGoingScenario;

    public static bool underAttack;

    public static string jsonDetail;

    // public static string saveObject;
    void Start()
    {
        if (SingletonScenarioManager != null)
        {
            Destroy(this.gameObject);
            return;
        }

        SingletonScenarioManager = this; // I am the singleton
        GameObject.DontDestroyOnLoad(this.gameObject); // Don't kil me

        pwdAtkController = GetComponent<PwdAtkController>();
        phishingController = GetComponent<PhishingController>();
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
                PwdAtkController.SetPasswordScenarioState (detail);
                break;
            case Scenario.Phishing:
                underAttack = true;
                break;
            case Scenario.Malware:
                underAttack = true;
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
                currentType = Scenario.Phishing;
                break;
            case Scenario.Malware:
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
            ScenarioFailed (detail);
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
                int i = TargetRandomizer(true);

                //Self phishing does not need following
                if (i == 0)
                {
                    phishingController.TriggerSelf();
                    Debug.Log("Trigger Self Phishing");
                    underAttack = false;
                    return (Scenario.None, "");
                }
                return (Scenario.None, phishingController.TriggerNPC(i));
            case Scenario.Malware:
                return (Scenario.Malware, "");
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
        onGoingScenario = Scenario.None;
        underAttack = false;
        EmailManager.ClearScenarioMails();
        ResultController.ShowSuccess (result, onGoingScenario);
        jsonDetail = null;
        GameManager.InvokeSaveData();
    }

    private static void ScenarioCompleted(string result)
    {
        onGoingScenario = Scenario.None;
        underAttack = false;
        EmailManager.ClearScenarioMails();
        ResultController.ShowSuccess (result, onGoingScenario);
        jsonDetail = null;
        GameManager.InvokeSaveData();
    }

    private static void ScenarioFailed(string result)
    {
        ResultController.ShowFailed (result, onGoingScenario);
        onGoingScenario = Scenario.None;
        EmailManager.ClearScenarioMails();
        underAttack = false;
        jsonDetail = null;
        GameManager.InvokeSaveData();
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
