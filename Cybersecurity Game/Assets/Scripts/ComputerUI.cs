using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUI : MonoBehaviour
{
    private static ComputerUI instance;

    public GameObject uiPanel;

    public GameObject notRespond;

    public GameObject errorScreen;

    public GameObject antivirus_prefab;

    public GameObject advertise_prefab;

    public GameObject trojan_prefab;

    public GameObject antivirus_App;

    public GameObject malwareReportPage;

    public ComputerManager computerManager;

    private GameObject taskBar;

    private GameObject software1;

    private GameObject software2;

    private GameObject antivirusIcon;

    //Screen
    public GameObject PlayerScreen;

    public GameObject NPCScreen;

    //Quotes use for showing error message
    public string[] bugQuote;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        ComputerManager.NewActiveComAction -= UpdateComState;
        ScenarioManager.ScenarioEnded -= CloseComputer;
    }

    public void StartComputer(int i)
    {
        computerManager.SetActiveComputer (i);
        if (ComputerManager.activeComputer.isInfected)
            malwareReportPage.SetActive(true);
        else
            malwareReportPage.SetActive(false);
        Target target = (Target) i;
        gameObject.SetActive(true);
        uiPanel.SetActive(false);
        notRespond.SetActive(false);
        GameObject activeCom = null;
        if (target == Target.You)
        {
            activeCom = PlayerScreen;
        }
        else
        {
            int index = (int) target - 1;
            NPCScreen.SetActive(true);
            activeCom = NPCScreen.transform.GetChild(index).gameObject;
        }
        activeCom.SetActive(true);
        taskBar = activeCom.transform.GetChild(0).gameObject;
        software1 = activeCom.transform.GetChild(1).gameObject;
        software2 = activeCom.transform.GetChild(2).gameObject;

        CheckAntivirusApp();
        CheckMaliciousState();
        ComputerManager.NewActiveComAction += UpdateComState;
        ScenarioManager.ScenarioEnded += CloseComputer;
    }

    public void UpdateComState()
    {
        if (ComputerManager.activeComputer.isInfected)
            malwareReportPage.SetActive(true);
        else
            malwareReportPage.SetActive(false);
        StopAllCoroutines();
        notRespond.SetActive(false);

        if (ScenarioManager.onGoingScenario == Scenario.Malware)
        {
            Debug.Log("bruh");
        }

        CheckMaliciousState();
    }

    public void CloseComputer()
    {
        NPCcontroller.ContinueNPCquest();
        CloseAllApps();
        Destroy (antivirusIcon);
        uiPanel.SetActive(true);
        PlayerScreen.SetActive(false);
        notRespond.SetActive(false);
        foreach (Transform child in NPCScreen.transform)
        {
            child.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void CloseAllApps()
    {
        foreach (GameObject
            app
            in
            GameObject.FindGameObjectsWithTag("Application")
        )
        {
            app.SetActive(false);
        }
    }

    public void OpenApplication(GameObject app)
    {
        if (ComputerManager.activeComputer.isSlow == true)
        {
            Debug.Log("Com is slow");
            StartCoroutine(ActiveDelay(app, 3));
        }
        else
        {
            StartCoroutine(ActiveDelay(app, 0.1f));
        }
    }

    IEnumerator ActiveDelay(GameObject app, float time)
    {
        app.SetActive(true);
        app.transform.SetAsLastSibling();
        notRespond.SetActive(true);
        notRespond.transform.SetAsLastSibling();
        yield return new WaitForSeconds(time);
        notRespond.SetActive(false);
    }

    private void CheckMaliciousState()
    {
        int malware = computerManager.CheckActiveComMalwareType();
        if (malware < 0) return;
        switch (malware)
        {
            case (int) MalwareType.Trojan:
                ShowTrojanIcon();
                StartCoroutine(ShowBugScreen());
                break;
            case (int) MalwareType.Virus:
                StartCoroutine(ShowBugScreen());
                break;
            case (int) MalwareType.Adware:
                StartCoroutine(ShowAdsScreen());
                break;
            default:
                break;
        }
    }

    IEnumerator ShowBugScreen()
    {
        Text errorText =
            errorScreen
                .transform
                .GetChild(0)
                .gameObject
                .transform
                .GetChild(1)
                .transform
                .GetComponent<Text>();
        for (; ; )
        {
            Debug.Log("new bug screen");
            float time = Random.Range(12f, 30f);
            int quoteIndex = Random.Range(0, bugQuote.Length);
            yield return new WaitForSeconds(time);
            if (!errorScreen.activeSelf)
            {
                errorScreen.SetActive(true);
                errorText.text = bugQuote[quoteIndex];
                errorScreen.transform.SetAsLastSibling();
            }
        }
    }

    private void CheckAntivirusApp()
    {
        if (ComputerManager.haveAntivirus == true)
        {
            antivirusIcon = Instantiate(antivirus_prefab) as GameObject;
            antivirusIcon.transform.SetParent(software1.transform, false);
            antivirusIcon.transform.SetAsLastSibling();
            antivirusIcon
                .GetComponent<Button>()
                .AddEventListener(antivirus_App, OpenApplication);
        }
    }

    IEnumerator ShowAdsScreen()
    {
        for (; ; )
        {
            float time = Random.Range(12f, 30f);

            GameObject ads = Instantiate(advertise_prefab) as GameObject;
            ads.transform.SetParent(this.transform, false);
            ads.transform.SetAsFirstSibling();

            yield return new WaitForSeconds(time);
        }
    }

    private void ShowTrojanIcon()
    {
        Debug.Log("Show trojan ");
        GameObject trojan = Instantiate(trojan_prefab) as GameObject;
        trojan.transform.SetParent(software1.transform, false);
        trojan.transform.SetAsLastSibling();
    }
}
