using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    public InputField login_passwordInput;

    public InputField regis_passwordInput;

    public GameObject emailContent;

    public GameObject mailHeader_prefab;

    public GameObject emailView;

    public GameObject linkHover;

    private List<EmailObject> emailInbox;

    private static string hoverLink;

    private static bool rememberPwd = false;

    public GameObject inbox;

    public GameObject login;

    public GameObject register;

    public GameObject resetPassword;

    public void OnEnable()
    {
        if (!string.IsNullOrEmpty(PasswordManager.password2))
        {
            OpenEmailLogin();
        }
        else
        {
            OpenEmailRegister();
        }
    }

    public void BackButton(int index)
    {
        GameObject email = GameObject.Find("Email");
        foreach (Transform child in email.transform)
        {
            child.gameObject.SetActive(false);
        }

        //Might have more cases in the future
        switch (index)
        {
            case 1:
                //Back to Email list page
                email.transform.Find("Inbox").gameObject.SetActive(true);
                ShowAllPlayerMails(); //This line is under testing
                break;
            default:
                Debug.Log("Index number needed for action");
                break;
        }
    }

    public void VerifyEmailLogin()
    {
        GameObject incorrectPasswordText =
            gameObject
                .transform
                .Find("Login")
                .transform
                .Find("IncorrectText")
                .gameObject;

        if (login_passwordInput.text.Equals(PasswordManager.password2))
        {
            Debug.Log("correct password");
            incorrectPasswordText.SetActive(false);
            OpenEmail();
        }
        else
        {
            Debug.Log("Wrong password");
            incorrectPasswordText.SetActive(true);
        }
    }

    private void OpenEmailLogin()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        login.SetActive(true);
        if (!rememberPwd) login_passwordInput.text = "";
    }

    private void OpenEmailRegister()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        register.SetActive(true);
    }

    public void RegisterEmailAccount()
    {
        PasswordManager.EditPassword(2, regis_passwordInput.text);
        EmailManager.ClearPlayerInbox();
        OpenEmailLogin();
    }

    private void OpenEmail()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }

        GameObject.Find("Bar").gameObject.SetActive(false);
        gameObject.transform.Find("Inbox").gameObject.SetActive(true);
        ShowAllPlayerMails();
    }

    private void ShowAllPlayerMails()
    {
        emailInbox = new List<EmailObject>(EmailManager.emailInbox);
        foreach (Transform child in emailContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = emailInbox.Count - 1; i >= 0; i--)
        {
            GameObject newMail = Instantiate(mailHeader_prefab) as GameObject;
            newMail.name = gameObject.name + "_" + i;
            newMail.transform.SetParent(emailContent.transform, false);

            Text newMail_sender =
                newMail.transform.GetChild(0).gameObject.GetComponent<Text>();
            newMail_sender.text = emailInbox[i].senderName;

            Text newMail_topic =
                newMail.transform.GetChild(1).gameObject.GetComponent<Text>();
            newMail_topic.text = emailInbox[i].topic;

            newMail.GetComponent<Button>().AddEventListener(i, ViewEmail);
            newMail
                .transform
                .GetChild(3)
                .GetComponent<Button>()
                .AddEventListener(i, DeleteMail);
        }
    }

    private void DeleteMail(int i)
    {
        Debug.Log(emailInbox[i].topic);
        EmailManager.DeleteEmailInbox (i);
        ShowAllPlayerMails();
    }

    private void ViewEmail(int i)
    {
        EmailObject emailDetail = emailInbox[i];
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        emailView.SetActive(true);

        Text view_senderName =
            emailView.transform.GetChild(1).gameObject.GetComponent<Text>();
        Text view_senderMail =
            emailView.transform.GetChild(2).gameObject.GetComponent<Text>();
        Text view_topic =
            emailView.transform.GetChild(3).gameObject.GetComponent<Text>();
        Text view_content =
            emailView.transform.GetChild(4).gameObject.GetComponent<Text>();

        view_senderName.text = emailDetail.senderName;
        view_senderMail.text = emailDetail.senderMail;
        view_topic.text = emailDetail.topic;
        view_content.text = emailDetail.content;
        emailView.transform.Find("Attachment").gameObject.SetActive(false);
        emailView.transform.Find("Attach link").gameObject.SetActive(false);
        if (emailDetail.link >= 0)
        {
            AttachmentObject attachmentDetail =
                EmailManager.GetAttachmentFromIndex(emailDetail.link);
            hoverLink = attachmentDetail.linkHover;
            GameObject attachment = null;
            if (attachmentDetail.isFile)
            {
                attachment = emailView.transform.Find("Attachment").gameObject;
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
                attachment = emailView.transform.Find("Attach link").gameObject;
                attachment.SetActive(true);
                Text attachment_text =
                    attachment.gameObject.GetComponent<Text>();
                attachment_text.text = attachmentDetail.linkName;
            }
            attachment
                .GetComponent<Button>()
                .AddEventListener(attachmentDetail.isFatal,
                this.AttachLinkAction);
        }
    }

    public void AttachHoverIn()
    {
        linkHover.SetActive(true);
        GameObject address = linkHover.transform.GetChild(0).gameObject;
        Text address_text = address.GetComponent<Text>();
        address_text.text = hoverLink;
    }

    public void AttachHoverOut()
    {
        linkHover.SetActive(false);
    }

    public void AttachLinkAction(bool isFatal)
    {
        if (isFatal)
        {
            PhishingController.InvokeScenarioFailure(true);
        }
        Debug.Log("I am safe");
    }

    /* --------- Controller Section ----------*/
    public void ResetPasswordPressed()
    {
        OpenPage (resetPassword);
    }

    public void OpenPage(GameObject pageObject)
    {
        CloseAllChild();
        pageObject.SetActive(true);
    }

    private void CloseAllChild()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            Debug.Log (child);
        }
    }

    public void RememberPassword(bool value)
    {
        rememberPwd = value;
    }
}
