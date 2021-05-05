using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    public GameObject result_Prefab;

    public string[] passwordTip_Success;

    public string[] passwordTip_Fail;

    public string[] phishingTip_Success;

    public string[] phishingTip_Fail;

    public string[] malwareTip_Success;

    public string[] malwareTip_Fail;

    public float failMultiplier;

    public int password_Rep;

    public int phishing_Rep;

    public int malware_Rep;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            Debug.Log("Resume Game");
        }
    }

    public static void ShowSuccess(string detail, Scenario currentScenario)
    {
        Debug.Log("IN SHOWSUCCESS: " + detail);
        Debug.Log("IN SHOWSUCCESS: " + currentScenario);
        Time.timeScale = 0f;
        switch (currentScenario)
        {
            case Scenario.Password:
                break;
            case Scenario.Phishing:
                break;
            case Scenario.Malware:
                break;
            case Scenario.Ransom:
                break;
            default:
                break;
        }
    }

    public static void ShowFailed(string detail, Scenario currentScenario)
    {
        Debug.Log("IN SHOWFAILED: " + detail);
        Debug.Log("IN SHOWFAILED: " + currentScenario);
        Time.timeScale = 0f;
        switch (currentScenario)
        {
            case Scenario.Password:
                break;
            case Scenario.Phishing:
                break;
            case Scenario.Malware:
                break;
            case Scenario.Ransom:
                break;
            default:
                break;
        }
    }

    private void PasswordScenario(string str, bool success)
    {
        PwdAtkObject save = JsonUtility.FromJson<PwdAtkObject>(str);
        PasswordScore score = save.complexity;
        Accounts account = save.target;
        string tip;

        string resultString = "bruh";
        int repChange;
        int repTotal;
        if (success)
        {
            tip =
                passwordTip_Success[Random
                    .Range(0, passwordTip_Success.Length)];
            repChange = password_Rep;
            repTotal = ReputationManager.Instance.ModifyReputation(repChange);
        }
        else
        {
            tip =
                passwordTip_Success[Random
                    .Range(0, passwordTip_Success.Length)];
            repChange = (int)(password_Rep * failMultiplier);
            repTotal = ReputationManager.Instance.ModifyReputation(-repChange);
        }
        string detailString =
            tip +
            "\nAttack target: " +
            account +
            " \tYour password strenght: " +
            score;
        Debug.Log (detailString);
        Debug.Log (resultString);
    }
}
