using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Email_ButtonExtension
{
    public static void AddEventListener<T>(
        this Button button,
        T param,
        Action<T> OnClick
    )
    {
        button
            .onClick
            .AddListener(delegate ()
            {
                OnClick (param);
            });
    }
}

public class Email : MonoBehaviour
{
    public InputField login_passwordInput;

    public InputField regis_passwordInput;

    public GameObject emailContent;

    public GameObject mailHeader_prefab;

    public GameObject emailView;

    private List<EmailObject> emailInbox;

    public void OnEnable()
    {
        if (PasswordManager.password2 != null)
        {
            OpenEmailLogin();
        }
        else
        {
            Debug.Log("to register page");
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
        gameObject.transform.Find("Login").gameObject.SetActive(true);
    }

    private void OpenEmail()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }

        GameObject.Find("Bar").SetActive(false);
        gameObject.transform.Find("Inbox").gameObject.SetActive(true);
        ShowAllPlayerMails();
    }

    private void ShowAllPlayerMails()
    {
        emailInbox = new List<EmailObject>(EmailManager.emailInbox);
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
        }
    }

    private void ViewEmail(int i)
    {
        EmailObject emailContent = emailInbox[i];
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

        view_senderName.text = emailContent.senderName;
        view_senderMail.text = emailContent.senderMail;
        view_topic.text = emailContent.topic;
        view_content.text = emailContent.content;

        if (emailContent.link == null)
        {
            emailView.transform.Find("Attachment").gameObject.SetActive(false);
            emailView.transform.Find("Attach link").gameObject.SetActive(false);
        }
    }
}
