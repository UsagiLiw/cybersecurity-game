using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskmanagerDashBoardController : MonoBehaviour
{
    public Image cpuUsageBar;

    public Image ramUsageBar;

    public Image diskUsageBar;

    public Text cpuUsageText;

    public Text ramUsageText;

    public Text diskUsageText;

    public Image downloadBar;

    public Text downloadText;

    public Image uploadBar;

    public Text uploadText;

    public GameObject panel_process5;

    private Computer computer;

    // Update is called once per frame
    void OnEnable()
    {
        computer = ComputerManager.activeComputer;
        StartCoroutine(DoUpdate());
        CheckMalwareProcess();
        CheckNetworkSpike();
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator DoUpdate()
    {
        for (; ; )
        {
            UpdatePerformance();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdatePerformance()
    {
        computer = ComputerManager.activeComputer;
        cpuUsageBar.fillAmount = computer.cpu;
        ramUsageBar.fillAmount = computer.ram;
        diskUsageBar.fillAmount = computer.disk;
        cpuUsageText.text = computer.cpu * 100 + "%";
        ramUsageText.text = computer.ram * 100 + "%";
        diskUsageText.text = computer.disk * 100 + "%";
    }

    private void CheckMalwareProcess()
    {
        panel_process5.SetActive(false);
        if (!ComputerManager.activeComputer.isInfected) return;

        MalwareSave malware = MalwareController.malwareSave;
        if (malware.malwareType == MalwareType.Virus)
        {
            string malName =
                MalwareManager.GetMalwareNameFromIndex(malware.dictIndex);
            panel_process5.SetActive(true);
            Text text5 =
                panel_process5.transform.GetChild(1).GetComponent<Text>();
            text5.text = malName;
        }
    }

    private void CheckNetworkSpike()
    {
        int index = ComputerManager.Instance.CheckActiveComMalwareType();
        float dValue = 0.57f;
        float uValue = 0.05f;
        if ((index == 0) || (index == 2))
        {
            dValue = 0.79f;
            uValue = 0.98f;
            downloadBar.fillAmount = dValue;
            downloadText.text = dValue * 100 + "Mbps";
            uploadBar.fillAmount = uValue;
            uploadText.text = uValue * 100 + "Mbps";
        }
        else
        {
            downloadBar.fillAmount = dValue;
            downloadText.text = dValue * 100 + "Mbps";
            uploadBar.fillAmount = uValue;
            uploadText.text = uValue * 100 + "Mbps";
        }
    }
}
