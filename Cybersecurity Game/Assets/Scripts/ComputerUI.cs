using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerUI : MonoBehaviour
{
    private static ComputerUI instance;

    public GameObject uiPanel;

    public Transform software1;

    public void CloseComputer()
    {
        CloseAllApps();
        uiPanel.SetActive(true);
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
        app.SetActive(true);
        app.transform.SetAsLastSibling();
    }

    // private void ParentTransfer(GameObject desktopIcon)
    // {
    //     desktopIcon.transform.parent = software1;
    // }
}
