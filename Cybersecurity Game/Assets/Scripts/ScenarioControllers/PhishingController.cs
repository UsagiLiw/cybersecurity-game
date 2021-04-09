using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingController : MonoBehaviour
{
    public static PhishingSave phishingSave;

    public string[] email_QuestDetail;

    public string[] web_QuestDetail;

    public void TriggerSelf()
    {
        int mailIndex = EmailManager.SendPhishingMail();
    }

    public string TriggerNPC(int index)
    {
        //Both mail and web phishing
        Target NPC = (Target) index;
        int rand = Random.Range(0, 1);
        bool isPhishing = Random.Range(0, 2) == 1 ? true : false;
        switch (rand)
        {
            case 0:
                return TriggerEmailCase(NPC, isPhishing);
            case 1:
                return TriggerWebCase(NPC, isPhishing);
            // return TriggerWebCase(NPC);
            default:
                Debug.Log("WTF - random range 0 to 1 and still fail?");
                return "";
        }
    }

    private string TriggerEmailCase(Target NPC, bool isPhishing)
    {
        string questDetail =
            email_QuestDetail[Random.Range(0, email_QuestDetail.Length - 1)];
        NPCcontroller
            .TriggerNPCPhishingQuest(NPC,
            Scenario.Phishing,
            questDetail,
            isPhishing);
        int index = 0;
        if (isPhishing) index = EmailManager.GetRandomPhishingMail();
        else index = EmailManager.GetRandomNormalMail();
        phishingSave =
            new PhishingSave {
                dayLeft = 4,
                atkType = AtkTypes.Email,
                questTarget = NPC,
                dictIndex = index,
                isPhishing = isPhishing
            };
        Debug
            .Log("Phishing EMAIL triggered, Target: " +
            NPC +
            " dictIndex: " +
            index);
        return JsonUtility.ToJson(phishingSave);
    }

    private string TriggerWebCase(Target NPC, bool isPhishing)
    {
        string questDetail =
            web_QuestDetail[Random.Range(0, web_QuestDetail.Length - 1)];
        NPCcontroller
            .TriggerNPCPhishingQuest(NPC,
            Scenario.Phishing,
            questDetail,
            isPhishing);
        int index = Random.Range(1, 4);
        phishingSave =
            new PhishingSave {
                dayLeft = 4,
                atkType = AtkTypes.Web,
                questTarget = NPC,
                dictIndex = index,
                isPhishing = isPhishing
            };
        Debug
            .Log("Phishing WEB triggered, Target: " +
            NPC +
            " dictIndex: " +
            index);
        return JsonUtility.ToJson(phishingSave);
    }

    public static PhishingSave GetPhishingDetail()
    {
        return phishingSave;
    }
}

public enum AtkTypes
{
    Email = 0,
    Web = 1
}

public class PhishingSave
{
    public int dayLeft; //Amount of day before the quest auto fail

    public AtkTypes atkType; //Target web or email

    public Target questTarget; //NPC target

    public bool isPhishing; //is legit or phishing

    public int dictIndex; //index to read from dictionary
}
