using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{
    [SerializeField] private Text sender;
    [SerializeField] private Text detail;

    public void SetContent(Notification content)
    {
        sender.text = content.sender;
        detail.text = content.detail;
    }

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
