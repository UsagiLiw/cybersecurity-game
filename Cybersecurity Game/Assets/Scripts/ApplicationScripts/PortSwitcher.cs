using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClosePortSwitcher()
    {
        gameObject.SetActive(false);
    }
}