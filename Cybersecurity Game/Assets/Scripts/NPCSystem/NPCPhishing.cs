using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCPhishing : MonoBehaviour
{
    private PhishingSave phishingDetail;

    private string hoverLink;

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
        hoverLink = null;
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

        phishingEmail.transform.GetChild(7).gameObject.SetActive(false);
        phishingEmail.transform.GetChild(8).gameObject.SetActive(false);
        if (emailContent.link >= 0)
        {
            AttachmentObject attachmentDetail =
                EmailManager.GetAttachmentFromIndex(emailContent.link);
            hoverLink = attachmentDetail.linkHover;
            GameObject attachment = null;
            if (attachmentDetail.isFile)
            {
                attachment =
                    phishingEmail.transform.Find("Attachment").gameObject;
                attachment.SetActive(true);
                GameObject attachment_image =
                    attachment.transform.GetChild(0).gameObject;
                Text attachment_text =
                    attachment
                        .transform
                        .GetChild(1)
                        .gameObject
                        .GetComponent<Text>();
                attachment_text.text = attachmentDetail.linkName;
            }
            else
            {
                attachment =
                    phishingEmail.transform.Find("Attach link").gameObject;
                attachment.SetActive(true);
                Text attachment_text =
                    attachment.gameObject.GetComponent<Text>();
                attachment_text.text = attachmentDetail.linkName;
            }
            // attachment
            //     .GetComponent<Button>()
            //     .AddEventListener(attachmentDetail.isFatal, AttachLinkAction);
        }
    }

    // public void AttachHoverIn()
    // {
    //     linkHover.SetActive(true);
    //     GameObject address = linkHover.transform.GetChild(0).gameObject;
    //     Text address_text = address.GetComponent<Text>();
    //     address_text.text = hoverLink;
    // }

    // public void AttachHoverOut()
    // {
    //     linkHover.SetActive(false);
    // }

    private void TriggerWebScenario(int webIndex)
    {
    }
}
