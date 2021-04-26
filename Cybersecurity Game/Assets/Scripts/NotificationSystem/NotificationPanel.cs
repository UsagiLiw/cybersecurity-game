using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{
    [SerializeField] private Text sender;
    [SerializeField] private Text detail;

    public Text Sender { get => sender; set => sender = value; }
    public Text Detail { get => detail; set => detail = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
