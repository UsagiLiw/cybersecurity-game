using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanAnimationController : MonoBehaviour
{
    public Image scanBar;
    public float scanTime;

    private GameObject homeScreen;
    private GameObject scanSecure;
    private GameObject scanUnsecure;

    private float timer = 0;

    private Computer computer;

    void Start()
    {
        computer = ComputerManager.activeComputer;
        homeScreen = GetComponent<Antivirus>().homeScreen;
        scanSecure = GetComponent<Antivirus>().scanSecure;
        scanUnsecure = GetComponent<Antivirus>().scanUnsecure;
    }

    void OnEnable()
    {
        scanBar.fillAmount = 0;
    }

    public void ScanButtonPressed()
    {
        Debug.Log("Scan button pressed");
        StartCoroutine(ScanAnimation());
    }

    IEnumerator ScanAnimation()
    {
        timer = 0;
        while (timer < scanTime)
        {
            timer += Time.deltaTime;
            scanBar.fillAmount = timer/scanTime;
            yield return null;
        }
        Debug.Log("Scan finished");
        ShowScanResult();
    }

    private void ShowScanResult()
    {
        homeScreen.SetActive(false);
        if(computer.isInfected == false)
        {
            scanUnsecure.SetActive(false);
            scanSecure.SetActive(true);
        }
        else if(computer.isInfected == true)
        {
            scanSecure.SetActive(false);
            scanUnsecure.SetActive(true);
        }
    }
}
