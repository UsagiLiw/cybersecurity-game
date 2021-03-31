using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortSwitcherController : MonoBehaviour
{
    [SerializeField]
    private PortPanel[] ports;

    private GameObject gameManager;
    private PortsManager portManager;

    private int availablePort;
    private int usedPort;
    private int closedPort;

    public Image usedChart;
    public Image closedChart;

    public Text totalNumber;
    public Text availableNumber;
    public Text usedNumber;
    public Text closedNumber;

    void Start() 
    {
        gameManager = GameObject.Find("GameManager");
        portManager = gameManager.GetComponent<PortsManager>();
        availablePort = portManager.GetPortStateNumber(PortState.Available);
        usedPort = portManager.GetPortStateNumber(PortState.Used);
        closedPort = portManager.GetPortStateNumber(PortState.Off);
        StartCoroutine("PortCheck");
    }

    IEnumerator PortCheck()
    {
        for (; ; )
        {
            UpdatePanels();
            UpdateChart();
            UpdateStatus();
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdatePanels()
    {

        for (int i = 0; i < ports.Length; i++)
        {
            UpdatePanel(i);
        }
    }

    private void UpdatePanel(int index)
    {
        PortPanel port = ports[index];

        PortState portState = portManager.GetPortState(index);
        port.portId.text = "PORT " + portManager.GetPortId(index);
        port.Address.text = portManager.GetPortAddress(index);
        port.State.text = portState.ToString();
        if (portState == PortState.Used)
        {
            port.State.color = new Color(1f, 0.3019608f, 0.2862745f);       //RGB value equals Hex #FF4D49           
        }
        else if (portState == PortState.Off)
        {
            port.State.color = Color.white;
        }
    }

    private void UpdateChart()
    {
        totalNumber.text = ports.Length.ToString();
        usedChart.fillAmount = (float) usedPort / ports.Length;
        closedChart.fillAmount = (float) closedPort / ports.Length;
    }

    private void UpdateStatus()
    {
        availableNumber.text = availablePort.ToString();
        usedNumber.text = usedPort.ToString();
        closedNumber.text = closedPort.ToString();
    }
}

[Serializable]
public class PortPanel
{
    public string port;
    public Text portId;
    public Text Address;
    public Text State;
}
