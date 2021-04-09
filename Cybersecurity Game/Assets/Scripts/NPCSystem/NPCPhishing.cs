using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCPhishing : MonoBehaviour
{
    public GameObject linkHover;

    public GameObject exitButton;

    public GameObject bar;

    public GameObject siteInfo;

    private PhishingSave phishingDetail;

    private GameObject UIPanel;

    private string hoverLink_string;

    private string bar_string;

    private string siteInfo_string;

    private void OnEnable()
    {
        Debug.Log("I am alive now");
        UIPanel = GameObject.FindGameObjectWithTag("UIPanel");
        UIPanel.SetActive(false);
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild(6).gameObject.SetActive(true);
        this.transform.GetChild(7).gameObject.SetActive(true);
        phishingDetail = PhishingController.GetPhishingDetail();
        siteInfo.SetActive(false);
        linkHover.SetActive(false);
        bar.SetActive(false);
        exitButton.SetActive(true);
        EnablePhishingTarget();
    }

    private void OnDisable()
    {
        UIPanel.SetActive(true);
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
        hoverLink_string = null;
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

        phishingEmail.transform.GetChild(5).gameObject.SetActive(false);
        phishingEmail.transform.GetChild(6).gameObject.SetActive(false);
        if (emailContent.link >= 0)
        {
            AttachmentObject attachmentDetail =
                EmailManager.GetAttachmentFromIndex(emailContent.link);
            hoverLink_string = attachmentDetail.linkHover;
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
            attachment
                .GetComponent<Button>()
                .AddEventListener(attachmentDetail.isFatal,
                this.AttachLinkAction1);
        }
    }

    public void AttachLinkAction1(bool isFatal)
    {
        if (isFatal)
        {
            Debug.Log("You die thankyou forever");
        }
        else
        {
            Debug.Log("I'm safe");
        }
    }

    private void TriggerWebScenario(int webIndex)
    {
    }

    public void HoverIn()
    {
        linkHover.SetActive(true);
        GameObject address = linkHover.transform.GetChild(0).gameObject;
        Text address_text = address.GetComponent<Text>();
        address_text.text = hoverLink_string;
    }

    public void HoverOut()
    {
        linkHover.SetActive(false);
    }

    public void CloseWindow()
    {
        NPCcontroller.ContinueNPCquest();
        Destroy(this.gameObject);
    }
}
