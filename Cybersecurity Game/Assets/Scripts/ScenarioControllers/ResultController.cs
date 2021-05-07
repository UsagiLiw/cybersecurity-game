using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    public static ResultController Instance { get; private set; }

    public GameObject result_Prefab;

    public Transform GUI;

    public string[] passwordTip;

    public string[] phishingTip_Email;

    public string[] phishingTip_Web;

    public string[] malwareTip_Virus;

    public string[] malwareTip_Trojan;

    public string[] malwareTip_Adware;

    public float failMultiplier;

    public int password_Rep;

    public int phishing_Rep;

    public int malware_Rep;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowSuccess(string detail, Scenario currentScenario)
    {
        Debug.Log("IN SHOWSUCCESS: " + detail);
        Debug.Log("IN SHOWSUCCESS: " + currentScenario);
        Time.timeScale = 0f;
        switch (currentScenario)
        {
            case Scenario.Password:
                PasswordScenario(detail, true);
                break;
            case Scenario.Phishing:
                PhishingScenario(detail, true);
                break;
            case Scenario.Malware:
                MalwareScenario(detail, true);
                break;
            case Scenario.Ransom:
                break;
            default:
                break;
        }
    }

    public void ShowFailed(string detail, Scenario currentScenario)
    {
        Debug.Log("IN SHOWFAILED: " + detail);
        Debug.Log("IN SHOWFAILED: " + currentScenario);
        Time.timeScale = 0f;
        switch (currentScenario)
        {
            case Scenario.Password:
                PasswordScenario(detail, false);
                break;
            case Scenario.Phishing:
                PhishingScenario(detail, false);
                break;
            case Scenario.Malware:
                MalwareScenario(detail, false);
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
        string tip = passwordTip[Random.Range(0, passwordTip.Length)];

        int repTotal;
        if (success)
        {
            repTotal =
                ReputationManager.Instance.ModifyReputation(password_Rep, 1);
        }
        else
        {
            repTotal =
                ReputationManager
                    .Instance
                    .ModifyReputation(-password_Rep, failMultiplier);
        }
        string detailString =
            tip +
            "\nAttack target: " +
            account +
            "\t\tYour password strength: " +
            score;

        GenerateResultScreen (success, detailString, repTotal);
    }

    private void PhishingScenario(string str, bool success)
    {
        PhishingSave save = JsonUtility.FromJson<PhishingSave>(str);
        AtkTypes atkType = save.atkType;
        string tip = "";
        int repTotal;
        switch (atkType)
        {
            case AtkTypes.Email:
                tip =
                    phishingTip_Email[Random
                        .Range(0, phishingTip_Email.Length)];
                break;
            case AtkTypes.Web:
                tip = phishingTip_Web[Random.Range(0, phishingTip_Web.Length)];
                break;
        }
        if (success)
        {
            repTotal =
                ReputationManager.Instance.ModifyReputation(phishing_Rep, 1);
        }
        else
        {
            repTotal =
                ReputationManager
                    .Instance
                    .ModifyReputation(-phishing_Rep, failMultiplier);
        }
        string detailString = tip + " \nThe target is a " + save.isPhishing;
        GenerateResultScreen (success, detailString, repTotal);
    }

    private void MalwareScenario(string str, bool success)
    {
        MalwareSave save = JsonUtility.FromJson<MalwareSave>(str);
        MalwareType type = save.malwareType;
        string tip = "";
        int repTotal;
        switch (type)
        {
            case MalwareType.Virus:
                tip =
                    malwareTip_Virus[Random.Range(0, malwareTip_Virus.Length)];
                break;
            case MalwareType.Trojan:
                tip =
                    malwareTip_Trojan[Random
                        .Range(0, malwareTip_Trojan.Length)];
                break;
            case MalwareType.Adware:
                tip =
                    malwareTip_Adware[Random
                        .Range(0, malwareTip_Adware.Length)];
                break;
            default:
                break;
        }
        if (success)
        {
            repTotal =
                ReputationManager.Instance.ModifyReputation(malware_Rep, 1);
        }
        else
        {
            repTotal =
                ReputationManager
                    .Instance
                    .ModifyReputation(malware_Rep, failMultiplier);
        }
        string detailString =
            tip +
            "\nMalware name: " +
            save.malwareName +
            "\t\tMalware type: " +
            type;
        GenerateResultScreen (success, detailString, repTotal);
    }

    private void GenerateResultScreen(
        bool status,
        string detailString,
        int repTotal
    )
    {
        string resultString =
            "Your current Reputation: " +
            repTotal +
            "\t\tBudget: " +
            BudgetManager.income +
            "/day";

        Debug.Log (detailString);
        Debug.Log (resultString);

        GameObject resultUI = Instantiate(result_Prefab) as GameObject;
        ResultUI script = resultUI.transform.GetComponent<ResultUI>();
        script.SetResult (status, detailString, resultString);
        resultUI.transform.SetParent (GUI);
    }
}
