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

    private Computer computer;

    // Update is called once per frame
    void OnEnable()
    {
        computer = ComputerManager.activeComputer;
        StartCoroutine(DoUpdate());
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
}
