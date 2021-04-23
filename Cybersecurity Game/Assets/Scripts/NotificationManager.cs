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

    public void SetNewNotification(Notification notification)
    {
        if(notificationList.Count >= 3)
        {
            notificationList.RemoveAt(0);
        }
        notificationList.Add(notification);
    }
}

[Serializable]
public class Notification
{
    public string sender;
    public string room;
    public string content;

    public Notification(string sender, string room, string content)
    {
        this.sender = sender;
        this.room = room;
        this.content = content;
    }
}
