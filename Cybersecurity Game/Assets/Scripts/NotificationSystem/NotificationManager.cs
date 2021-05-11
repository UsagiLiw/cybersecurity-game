using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static List<Notification> notifications = new List<Notification>();

    public delegate void NotificationHandler(Notification notification);
    public static event NotificationHandler NewNotification;

    public static void SetNewNotification(Notification notification)
    {
        if (notifications.Count >= 3)
        {
            notifications.RemoveAt(0);
        }
        notifications.Add(notification);
        NewNotification?.Invoke(notification);
        FindObjectOfType<AudioManager>().Play("sfx_notification");
        // Debug.Log("New notification added : " + notification.sender + " " + notification.detail);
        // Debug.Log("Current noti list size: " + notifications.Count);
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
