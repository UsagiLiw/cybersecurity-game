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

    public string[] malwareTip_Ransom;

    public float failMultiplier;

    public int password_Rep;

    public int phishing_Rep;

    public int malware_Rep;

    public int ransom_Rep;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowSuccess(string detail, Scenario currentScenario)
    {
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
            "Tip: " +
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
        string isPhishing = save.isPhishing ? "Phishing" : "Legit";
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
        string detailString = "Tip: " + tip + " \nThe target is " + isPhishing;
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
                    .ModifyReputation(-malware_Rep, failMultiplier);
        }
        string detailString =
            "Tip: " +
            tip +
            "\nMalware name: " +
            save.malwareName +
            "\t\tMalware type: " +
            type;
        GenerateResultScreen (success, detailString, repTotal);
    }

    private void RansomScenario(string str, bool success)
    {
        MalwareSave save = JsonUtility.FromJson<MalwareSave>(str);
        string tip =
            malwareTip_Ransom[Random.Range(0, malwareTip_Ransom.Length)];
        int repTotal;
        if (success)
        {
            repTotal =
                ReputationManager.Instance.ModifyReputation(ransom_Rep, 1);
        }
        else
        {
            if (save.dictIndex == 1)
                repTotal =
                    ReputationManager
                        .Instance
                        .ModifyReputation(-malware_Rep, 1);
            else
                repTotal =
                    ReputationManager.Instance.ModifyReputation(-ransom_Rep, 1);
        }
        string detailString =
            "Tip: " +
            tip +
            "\nMalware name: " +
            save.malwareName +
            "\t\tMalware type: " +
            save.malwareType;
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
        bool gameOver = false;
        if (repTotal <= 0) gameOver = true;
        GameObject resultUI = Instantiate(result_Prefab) as GameObject;
        ResultUI script = resultUI.transform.GetComponent<ResultUI>();
        script.SetResult (status, detailString, resultString, gameOver);
        resultUI.transform.SetParent(GUI, false);
        resultUI.transform.SetAsLastSibling();
        Time.timeScale = 0f;
    }
}
