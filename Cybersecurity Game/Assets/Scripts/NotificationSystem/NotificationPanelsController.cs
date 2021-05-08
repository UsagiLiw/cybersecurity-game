using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPanelsController : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private List<GameObject> notificationPanels = new List<GameObject>();

    private void OnEnable()
    {
        NotificationManager.NewNotification += InstantiateNotification;
    }

    private void InstantiateNotification(Notification notification)
    {
        if(notificationPanels.Count >= 3)
        {
            notificationPanels.RemoveAt(0);
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
        }
        GameObject _notification = Instantiate(notificationPrefab) as GameObject;
        _notification.transform.SetParent(this.gameObject.transform, false);
        _notification.transform.SetAsFirstSibling();
        NotificationPanel notificationPanel = _notification.gameObject.GetComponentInChildren<NotificationPanel>();
        notificationPanel.SetContent(notification);
        notificationPanels.Add(_notification);

        FindObjectOfType<AudioManager>().Play("sfx_notification");
    }

    private void OnDisable()
    {
        NotificationManager.NewNotification -= InstantiateNotification;
    }
}
