using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public List<Notification> notificationList = new List<Notification>();

    public delegate void NotificationHandler(Notification notification);
    public static event NotificationHandler NewNotification;


    void Start()
    {
        
    }

    public void SetNewNotification(Notification notification)
    {
        if(notificationList.Count >= 3)
        {
            notificationList.RemoveAt(0);
        }
        notificationList.Add(notification);
        NewNotification.Invoke(notification);
    }
}

[Serializable]
public class Notification
{
    public string sender;
    public string detail;

    public Notification(string sender, string detail)
    {
        this.sender = sender;
        this.detail = detail;
    }
}
