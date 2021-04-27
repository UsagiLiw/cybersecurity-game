using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPanelsController : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private List<NotificationPanel> notificationPanels = new List<NotificationPanel>();


    private void OnEnable()
    {
        NotificationManager.NewNotification += InstantiateNotification;
    }
    void Start()
    {
        
    }

    private void InstantiateNotification(Notification notification)
    {
        GameObject _notification = Instantiate(notificationPrefab) as GameObject;
        NotificationPanel notificationPanel = _notification.GetComponent<NotificationPanel>();
        notificationPanel.SetContent(notification);
    }

    // Update is called once per frame
    void Update()
    {
        NotificationManager.NewNotification -= InstantiateNotification;
    }
}
