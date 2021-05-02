using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public delegate void NotificationHandler(Notification notification);
    public static event NotificationHandler NewNotification;

    public static void SetNewNotification(Notification notification)
    {
        NewNotification?.Invoke(notification);
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
