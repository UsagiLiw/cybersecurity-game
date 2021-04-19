using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    private static ComputerUI instance;

    public GameObject uiPanel;

    public GameObject notRespond;

    public Transform software1;

    public ComputerManager computerManager;

    //Screen
    public GameObject PlayerScreen;

    public GameObject NPCScreen;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StartComputer(int i)
    {
        computerManager.SetActiveComputer (i);
        Target target = (Target) i;
        gameObject.SetActive(true);
        uiPanel.SetActive(false);
        notRespond.SetActive(false);

        if (target == Target.You)
        {
            PlayerScreen.SetActive(true);
        }
        else
        {
            int index = (int) target;
            NPCScreen.SetActive(true);
            NPCScreen.transform.GetChild(index).gameObject.SetActive(true);
        }
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
        if (computerManager.ActiveComIsSlow() == true)
        {
            Debug.Log("Com is slow");
            StartCoroutine(ActiveDelay(app, 3));
        }
        else
        {
            StartCoroutine(ActiveDelay(app, 0.5f));
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

    // private void ParentTransfer(GameObject desktopIcon)
    // {
    //     desktopIcon.transform.parent = software1;
    // }
}
