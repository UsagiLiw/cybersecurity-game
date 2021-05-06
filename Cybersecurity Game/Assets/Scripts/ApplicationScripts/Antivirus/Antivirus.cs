using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antivirus : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject scanSecure;
    public GameObject scanUnsecure;

    public void OnEnable()
    {
        SetBackToHome();
    }

    public void CloseAntivirus()
    {
        SetBackToHome();
        gameObject.SetActive(false);
    }

    private void SetBackToHome()
    {
        homeScreen.SetActive(true);
        scanSecure.SetActive(false);
        scanUnsecure.SetActive(false);
    }

    public void OpenSecurePage()
    {
        homeScreen.SetActive(false);
        scanSecure.SetActive(true);
        scanUnsecure.SetActive(false);
    }
}
