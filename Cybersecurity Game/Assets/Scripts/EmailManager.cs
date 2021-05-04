using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    private readonly string email_file = "EmailTemplate.json";

    private readonly string scenario_file = "ScenarioEmail.json";

    private readonly string attachment_file = "AttachmentDictionary.json";

    private static EmailObject[] emailDict;

    private static EmailObject[] scenarioDict;

    private static AttachmentObject[] attachmentDict;

    public static List<EmailObject> emailInbox;

    // public static List<int> indexInbox;
    // public static List<int> scenarioInbox;
    public static List<bool> indexInbox_Read;

    public static List<bool> scenarioInbox_Read;

    public void SetPlayerInbox(
        int[] mailIndex,
        int[] scenarioIndex,
        bool[] read1,
        bool[] read2
    )
    {
        indexInbox_Read = read1.OfType<bool>().ToList();
        scenarioInbox_Read = read2.OfType<bool>().ToList();

        int i = 0;
        emailInbox = new List<EmailObject>();
        Debug.Log(mailIndex.Length);
        foreach (int index in mailIndex)
        {
            emailInbox.Add(emailDict[index]);
            emailInbox[i].read = indexInbox_Read[i];
            i++;
        }
        int j = 0;
        foreach (int index in scenarioIndex)
        {
            emailInbox.Add(scenarioDict[index]);
            emailInbox[i].read = scenarioInbox_Read[j];
            j++;
        }
    }

    public void SetDictionaries()
    {
        emailDict = SetEmailDictionary(email_file);
        scenarioDict = SetEmailDictionary(scenario_file);
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

    public static (int[], int[]) ReturnInboxIndex()
    {
        List<int> email1 = new List<int>();
        List<int> email2 = new List<int>();

        foreach (EmailObject o in emailInbox)
        {
            if (!o.scenario)
                email1.Add(o.index);
            else
                email2.Add(o.index);
        }
        return (email1.ToArray(), email2.ToArray());
    }

    public static (bool[], bool[]) ReturnInboxRead()
    {
        List<bool> read1 = new List<bool>();
        List<bool> read2 = new List<bool>();

        foreach (EmailObject o in emailInbox)
        {
            if (!o.scenario)
                read1.Add(o.read);
            else
                read2.Add(o.read);
        }
        return (read1.ToArray(), read2.ToArray());
    }

    public void SendRandomMail()
    {
        int templateLength = emailDict.Length - 1;
        int index = Random.Range(0, templateLength);

        emailInbox.Add(emailDict[index]);

        // indexInbox.Add (index);
        if (emailInbox.Count > 20)
        {
            // indexInbox.RemoveAt(0);
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
    }

    public static int SendPhishingMail()
    {
        int index = GetRandomPhishingMail();
        emailInbox.Add(scenarioDict[index]);
        return index;
    }

    public static int GetRandomPhishingMail()
    {
        int templateLength = scenarioDict.Length - 1;
        int index = Random.Range(2, templateLength);
        return index;
    }

    public static int GetRandomNormalMail()
    {
        int templateLength = emailDict.Length - 1;
        int index = Random.Range(0, templateLength);
        return index;
    }

    public static EmailObject GetMailFromIndex(int index, bool phishing)
    {
        if (phishing) return scenarioDict[index];

        return emailDict[index];
    }

    public static AttachmentObject GetAttachmentFromIndex(int index)
    {
        return attachmentDict[index];
    }

    public static void ClearScenarioMails()
    {
        for (int i = emailInbox.Count() - 1; i >= 0; i--)
        {
            if (emailInbox[i].scenario) emailInbox.RemoveAt(i);
        }
    }

    public static void ClearPlayerInbox()
    {
        emailInbox.Clear();
    }

    public static void DeleteEmailInbox(int i)
    {
        emailInbox.RemoveAt (i);
        GameManager.InvokeSaveData();
    }

    public static void MarkAsRead(int i)
    {
        emailInbox[i].read = true;
    }
}
