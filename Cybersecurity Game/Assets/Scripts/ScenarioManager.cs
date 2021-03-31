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

    public static scenario onGoingScenario;

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

    public (scenario, string) CheckStatus()
    {
        int index = 0;
        if (!underAttack)
        {
            index = ScenarioRandomizer();
            underAttack = true;
            onGoingScenario = (scenario) index;
            return TriggerScenario(index);
        }
        else
        {
            return UpdateScenarioStatus();
        }
    }

    public void SetScenarioState(scenario currentScenario, string detail)
    {
        if (currentScenario != scenario.None && String.IsNullOrEmpty(detail))
        {
            Debug.Log("Warning, On going scenario but detail is empty");
            onGoingScenario = scenario.None;
        }
        else
        {
            onGoingScenario = currentScenario;
        }
        switch (onGoingScenario)
        {
            case scenario.None:
                underAttack = false;
                break;
            case scenario.Password:
                underAttack = true;
                PwdAtkController.SetPasswordScenarioState (detail);
                break;
            case scenario.Phishing:
                underAttack = true;
                break;
            case scenario.Malware:
                underAttack = true;
                break;
            default:
                throw new InvalidOperationException("Error: Unknown scenario index: " +
                    onGoingScenario);
        }
    }

    private (scenario, string) UpdateScenarioStatus()
    {
        string detail = null;
        scenario currentType;
        switch (onGoingScenario)
        {
            case scenario.None:
                underAttack = false;
                currentType = scenario.None;
                break;
            case scenario.Password:
                (underAttack, detail) = pwdAtkController.UpdateScenarioState();
                currentType = scenario.Password;
                break;
            case scenario.Phishing:
                currentType = scenario.Phishing;
                break;
            case scenario.Malware:
                currentType = scenario.Malware;
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
            return (scenario.None, null);
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

    private (scenario, string) TriggerScenario(int chosenScenario)
    {
        underAttack = true;
        switch ((scenario) chosenScenario)
        {
            case scenario.None:
                Debug
                    .Log("Warning: Trigger scenario.None - Should not enter this case at all");
                return (scenario.None, "");
            case scenario.Password:
                return (
                    scenario.Password,
                    pwdAtkController.CheckVulnerability()
                );
            case scenario.Phishing:
                return (scenario.Phishing, "");
            case scenario.Malware:
                return (scenario.Malware, "");
            default:
                throw new InvalidOperationException("Error: Unknown scenario index: " +
                    chosenScenario);
        }
    }

    public static void InvokeScenarioSuccess(string result)
    {
        onGoingScenario = scenario.None;
        underAttack = false;
        jsonDetail = null;
        ResultController.ShowSuccess (result, onGoingScenario);
        GameManager.InvokeSaveData();
    }

    private static void ScenarioCompleted(string result)
    {
        onGoingScenario = scenario.None;
        underAttack = false;
        ResultController.ShowSuccess (result, onGoingScenario);
        GameManager.InvokeSaveData();
    }

    private static void ScenarioFailed(string result)
    {
        ResultController.ShowFailed (result, onGoingScenario);
        onGoingScenario = scenario.None;
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
