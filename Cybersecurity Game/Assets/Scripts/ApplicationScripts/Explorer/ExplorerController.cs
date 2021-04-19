using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplorerController : MonoBehaviour
{
    public Image drive1_bar;

    public Text drive1_text;

    public Image drive2_bar;

    public Text drive2_text;

    public Image drive3_bar;

    public Text drive3_text;

    private Computer computer;

    void Start()
    {
        computer = ComputerManager.activeComputer;
        StartCoroutine("DriveCheck");
    }

    IEnumerator DriveCheck()
    {
        for (; ; )
        {
            updateDrives();
            yield return new WaitForSeconds(5f);
        }
    }

    public void CloseExplorer()
    {
        gameObject.SetActive(false);
    }

    private void updateDrives()
    {
        drive1_bar.fillAmount = (computer.driveC / 200f);
        drive2_bar.fillAmount = (computer.driveD / 400f);
        drive3_bar.fillAmount = (computer.driveE / 400f);
        drive1_text.text = 200 - computer.driveC + " GB free of 200 GB";
        drive2_text.text = 400 - computer.driveD + " GB free of 400 GB";
        drive3_text.text = 400 - computer.driveE + " GB free of 400 GB";
    }
}
