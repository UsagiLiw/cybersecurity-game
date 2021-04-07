using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    public string email_file;

    public string scenario_file;

    public string phishing_file;

    public string attachment_file;

    private static EmailObject[] emailDict;

    private static EmailObject[] scenarioDict;

    private static EmailObject[] phishingDict;

    private static AttachmentObject[] attachmentDict;

    public static List<EmailObject> emailInbox;

    public static List<int> indexInbox;

    public static List<int> scenarioInbox;

    public void SetPlayerInbox(int[] mailIndex, int[] scenarioIndex)
    {
        indexInbox = mailIndex.OfType<int>().ToList();
        scenarioInbox = scenarioIndex.OfType<int>().ToList();
        emailInbox = new List<EmailObject>();
        foreach (int index in mailIndex)
        {
            emailInbox.Add(emailDict[index]);
        }
        foreach (int index in scenarioIndex)
        {
            emailInbox.Add(scenarioDict[index]);
        }
    }

    public void SetDictionaries()
    {
        emailDict = SetEmailDictionary(email_file);
        scenarioDict = SetEmailDictionary(scenario_file);

        // phishingDict = SetEmailDictionary(phishing_file);
        attachmentDict = SetAttachmentDictionary(attachment_file);
    }

    private EmailObject[] SetEmailDictionary(string fileName)
    {
        string jsonString = SaveSystem.LoadDictionary(fileName);
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find " + fileName);
            Application.Quit();
        }
        return JsonHelper.FromJson<EmailObject>(jsonString);
    }

    private AttachmentObject[] SetAttachmentDictionary(string fileName)
    {
        string jsonString = SaveSystem.LoadDictionary(fileName);
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find " + fileName);
            Application.Quit();
        }
        return JsonHelper.FromJson<AttachmentObject>(jsonString);
    }

    public void SendRandomMail()
    {
        int templateLength = emailDict.Length - 1;
        int index = Random.Range(0, templateLength);

        emailInbox.Add(emailDict[index]);
        indexInbox.Add (index);

        if (indexInbox.Count > 20)
        {
            indexInbox.RemoveAt(0);
            emailInbox.RemoveAt(0);
        }
    }

    public static void SendScenarioMail(int index)
    {
        Debug.Log("SendSceMail: " + index);
        if (index > scenarioDict.Length - 1 || index < 0)
        {
            Debug.Log("Error, Send scenario mail index out of range");
            return;
        }
        emailInbox.Add(scenarioDict[index]);
        scenarioInbox.Add (index);
    }

    public static int SendPhishingMail()
    {
        int index = GetRandomPhishingMail();

        emailInbox.Add(phishingDict[index]);

        scenarioInbox.Add (index);
        return index;
    }

    public static int GetRandomPhishingMail()
    {
        int templateLength = scenarioDict.Length - 1;
        int index = Random.Range(2, templateLength);

        return index;
    }

    public static EmailObject GetMailFromIndex(int index, bool phishing)
    {
        if (phishing)
        {
            return scenarioDict[index];
        }
        return emailDict[index];
    }

    public static AttachmentObject GetAttachmentFromIndex(int index)
    {
        return attachmentDict[index];
    }

    public static void ClearScenarioMails()
    {
        scenarioInbox.Clear();
    }
}
