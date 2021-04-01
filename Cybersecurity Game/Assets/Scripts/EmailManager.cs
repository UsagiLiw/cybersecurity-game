using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    private static EmailObject[] emailDict;

    private static EmailObject[] scenarioDict;

    private static EmailObject[] phishingMailDict;

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
        SetMailDictionary();
        SetScenarioMailDictionary();
        SetPhishingMailDictionary();
    }

    private void SetMailDictionary()
    {
        string jsonString = SaveSystem.LoadDictionary("EmailTemplate.json");
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find EmailTemplate.json");
            Application.Quit();
        }
        emailDict = JsonHelper.FromJson<EmailObject>(jsonString);
    }

    private void SetScenarioMailDictionary()
    {
        string jsonString = SaveSystem.LoadDictionary("ScenarioEmail.json");
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find ScenarioEmail.json");
            Application.Quit();
        }
        scenarioDict = JsonHelper.FromJson<EmailObject>(jsonString);
    }

    private void SetPhishingMailDictionary()
    {
        string jsonString =
            SaveSystem.LoadDictionary("PhishingDictionary.json");
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find ScenarioEmail.json");
            Application.Quit();
        }
        phishingMailDict = JsonHelper.FromJson<EmailObject>(jsonString);
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
        return 0;
    }

    public static void ClearScenarioMails()
    {
        scenarioInbox = null;
    }
}
