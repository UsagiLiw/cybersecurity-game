using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class ScenarioManager : MonoBehaviour
{
    static ScenarioManager SingletonScenarioManager;

    private PwdAtkController pwdAtkController;

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
    }

    public (Scenario, string) CheckStatus()
    {
        int index = 0;
        if (!underAttack)
        {
            index = ScenarioRandomizer();
            underAttack = true;
            onGoingScenario = (Scenario) index;
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

        //sceContinue> true = scenario still on and continue, false = player failed the scenario
        //
        if (underAttack)
        {
            //DO SOMETHING
            return (currentType, detail);
        }
        else
        {
            //DO SOMETHING
            ScenarioFailed (detail);
            return (Scenario.None, null);
        }
    }

    private int ScenarioRandomizer()
    {
        int i = Random.Range(0, 100);
        Debug.Log("Sc random result: " + i);
        for (int j = 0; j < scenarioTypes.Length - 1; j++)
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
                return j + 1;
            }
        }
        return 0;
        // throw new InvalidOperationException("Error in ScenarioRandomizer, random process failed");
    }

    private (Scenario, string) TriggerScenario(int chosenScenario)
    {
        underAttack = true;
        switch ((Scenario) chosenScenario)
        {
            case Scenario.None:
                Debug
                    .Log("Warning: Trigger scenario.None - Should not enter this case at all");
                return (Scenario.None, "");
            case Scenario.Password:
                return (
                    Scenario.Password,
                    pwdAtkController.CheckVulnerability()
                );
            case Scenario.Phishing:
                int i = TargetRandomizer(true);
                return (Scenario.Phishing, "");
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
        if (self)
        {
            i = 1;
        }
        return Random.Range(i, targetCount);
    }

    public static void InvokeScenarioSuccess(string result)
    {
        onGoingScenario = Scenario.None;
        underAttack = false;
        jsonDetail = null;
        ResultController.ShowSuccess (result, onGoingScenario);
        GameManager.InvokeSaveData();
    }

    private static void ScenarioCompleted(string result)
    {
        onGoingScenario = Scenario.None;
        underAttack = false;
        ResultController.ShowSuccess (result, onGoingScenario);
        GameManager.InvokeSaveData();
    }

    private static void ScenarioFailed(string result)
    {
        ResultController.ShowFailed (result, onGoingScenario);
        onGoingScenario = Scenario.None;
        underAttack = false;
        GameManager.InvokeSaveData();
    }
}

[System.Serializable]
public class ScenarioClass
{
    public string scenarioType;

    public int minProbRange = 0;

    public int maxProbRange = 0;
}
