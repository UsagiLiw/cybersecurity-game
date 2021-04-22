using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PortState {Off, Available, Used};
public class PortsManager : MonoBehaviour
{
    [SerializeField]
    public Port[] ports = new Port[16];

    public void SetPortState (int index, int state)
    {
        ports[index].state = (PortState) state;
    }

    public void SetPortState(int index, PortState state)
    {
        ports[index].state = state;
    }

    public int GetPortSize()
    {
        return ports.Length;
    }

    public PortState GetPortState(int index)
    {
        return ports[index].state;
    }

    public int GetPortId(int index)
    {
        return ports[index].portId;
    }

    public string GetPortAddress(int index)
    {
        return ports[index].address;
    }

    /* Return number of ports in the specific state*/
    public int GetPortStateNumber(PortState portState)
    {
        int count = 0;
        for (int i = 0; i < ports.Length; i++)
        {
            if (ports[i].state == portState)
                count++;
        }
        return count;
    }
}

[Serializable]
public class Port
{
    public string portIdText;
    public int portId;
    public string address;
    public PortState state;
}