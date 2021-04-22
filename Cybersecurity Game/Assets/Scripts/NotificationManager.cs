using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public List<Notification> notificationList = new List<Notification>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class Notification
{
    public string sender;
    public string room;
    public string content;
}
