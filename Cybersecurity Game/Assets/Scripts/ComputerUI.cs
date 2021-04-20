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

    public ComputerManager computerManager;

    private GameObject taskBar;

    private GameObject software1;

    private GameObject software2;

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
        notRespond.SetActive(false);
        StopAllCoroutines();
    }

    public void StartComputer(int i)
    {
        computerManager.SetActiveComputer (i);
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
            int index = (int) target;
            NPCScreen.SetActive(true);
            activeCom = NPCScreen.transform.GetChild(index).gameObject;
        }
        activeCom.SetActive(true);
        taskBar = activeCom.transform.GetChild(0).gameObject;
        software1 = activeCom.transform.GetChild(1).gameObject;
        software2 = activeCom.transform.GetChild(2).gameObject;
        CheckAntivirusApp();
        CheckBugState();
    }

    public void CloseComputer()
    {
        CloseAllApps();
        uiPanel.SetActive(true);
        PlayerScreen.SetActive(false);
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
            StartCoroutine(ActiveDelay(app, 0.2f));
        }
    }

    IEnumerator ActiveDelay(GameObject app, float time)
    {
        notRespond.SetActive(true);
        notRespond.transform.SetAsLastSibling();
        yield return new WaitForSeconds(time);
        notRespond.SetActive(false);
        app.SetActive(true);
        app.transform.SetAsLastSibling();
    }

    private void CheckBugState()
    {
        if (ComputerManager.activeComputer.isBuggy == true)
        {
            StartCoroutine(ShowBugScreen());
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
            software1.transform.Find("Antivirus").gameObject.SetActive(true);
        }
    }
}
