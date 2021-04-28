using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortSwitch : MonoBehaviour
{
    [SerializeField] private int portId;
    [SerializeField] private Text portNumber;
    [SerializeField] private Text address;
    [SerializeField] private Text state;
    [SerializeField] private Text buttonText;

    private PortsManager portManager;

    void Awake()
    {
        portManager = GameObject.Find("GameManager").GetComponent<PortsManager>();
    }

    void Start()
    {
        UpdatePanel();
    }

    public void SwitchPort()
    {
        PortState portState = portManager.ports[portId].state;
        if (portState == PortState.Off)
        {
            portManager.SetPortState(portId, PortState.Available);
        }
        else
        {
            portManager.SetPortState(portId, PortState.Off);
        }
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        PortState portState = portManager.GetPortState(portId);

        portNumber.text = "PORT " + portManager.GetPortId(portId);
        address.text = portManager.GetPortAddress(portId);
        state.text = portState.ToString();

        if (portState == PortState.Used)
        {
            state.color = new Color(1f, 0.3019608f, 0.2862745f);       //RGB value equals Hex #FF4D49 RED
            buttonText.text = "Off";
        }
        else if (portState == PortState.Off)
        {
            state.color = Color.white;
            buttonText.text = "On";
        }
        else if (portState == PortState.Available)
        {
            state.color = new Color(0.2862745f, 1f, 0.4f);             //RGB value equals Hex #49FF66 GREEN
            buttonText.text = "Off";
        }
    }
}
