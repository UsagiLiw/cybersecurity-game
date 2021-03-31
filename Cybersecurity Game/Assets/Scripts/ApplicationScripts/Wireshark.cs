using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wireshark : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseWireshark()
    {
        gameObject.SetActive(false);
    }
}