using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskmanagerController : MonoBehaviour
{

    public GameObject homePage;
    public GameObject processPage;
    // Start is called before the first frame update
    void OnEnable()
    {
        homePage.SetActive(true);
        processPage.SetActive(false);
    }

    public void CloseTaskManager()
    {
        gameObject.SetActive(false);
    }

    public void selectProcess()
    {
        processPage.SetActive(true);
        homePage.SetActive(false);   
    }

    public void selectHome()
    {
        homePage.SetActive(true);
        processPage.SetActive(false);
    }
}
