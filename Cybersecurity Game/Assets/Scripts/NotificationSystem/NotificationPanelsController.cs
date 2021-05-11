using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPanelsController : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private List<GameObject> notificationPanels = new List<GameObject>();

    private void OnEnable()
    {
        foreach (Notification n in NotificationManager.notifications)
        {
            InstantiateNotification(n);
        }

        NotificationManager.NewNotification += InstantiateNotification;

    }

    private void InstantiateNotification(Notification notification)
    {
        if(notificationPanels.Count >= 3)
        {
            notificationPanels.RemoveAt(notificationPanels.Count - 1) ;
            Destroy(this.gameObject.transform.GetChild(2).gameObject);
        }
        GameObject _notification = Instantiate(notificationPrefab) as GameObject;
        _notification.transform.SetParent(this.gameObject.transform, false);
        _notification.transform.SetAsFirstSibling();
        NotificationPanel notificationPanel = _notification.gameObject.GetComponentInChildren<NotificationPanel>();
        notificationPanel.SetContent(notification);
        notificationPanels.Add(_notification);

        Debug.Log("Noti panel instantiated : " + notification.sender + notification.detail);
        Debug.Log("noti panel list size : " + notificationPanels.Count);
    }

    private void ClearInstance()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        notificationPanels.Clear();
    }

    private void OnDisable()
    {
        ClearInstance();
        NotificationManager.NewNotification -= InstantiateNotification;
    }
}
