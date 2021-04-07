using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCPhishing : MonoBehaviour
{
    private PhishingSave phishingDetail;

    private void Start()
    {
        Debug.Log("I am alive now");
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild(6).gameObject.SetActive(true);
        phishingDetail = PhishingController.GetPhishingDetail();
        EnablePhishingTarget();
    }

    private void EnablePhishingTarget()
    {
        switch (phishingDetail.atkType)
        {
            case AtkTypes.Email:
                TriggerEmailScenario(phishingDetail.dictIndex);
                break;
            case AtkTypes.Web:
                TriggerWebScenario(phishingDetail.dictIndex);
                break;
        }
    }

    private void TriggerEmailScenario(int index)
    {
        Debug.Log("Open Email fish");
        EmailObject emailContent = EmailManager.GetMailFromIndex(index, true);
        GameObject phishingEmail = this.transform.GetChild(0).gameObject;
        phishingEmail.SetActive(true);

        Text senderName =
            phishingEmail
                .transform
                .Find("Sender")
                .gameObject
                .GetComponent<Text>();
        Text senderAddress =
            phishingEmail
                .transform
                .Find("SenderAddress")
                .gameObject
                .GetComponent<Text>();
        Text topic =
            phishingEmail
                .transform
                .Find("Topic")
                .gameObject
                .GetComponent<Text>();
        Text content =
            phishingEmail
                .transform
                .Find("Content")
                .gameObject
                .GetComponent<Text>();

        senderName.text = emailContent.senderName;
        senderAddress.text = emailContent.senderMail;
        topic.text = emailContent.topic;
        content.text = emailContent.content;
    }

    private void TriggerWebScenario(int webIndex)
    {
    }
}
