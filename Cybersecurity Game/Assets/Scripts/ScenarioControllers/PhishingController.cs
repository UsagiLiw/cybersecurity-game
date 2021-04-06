using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingController : MonoBehaviour
{
    public static PhishingSave phishingSave;

    public string[] email_QuestDetail;

    public string[] web_QuestDetail;

    public enum AtkTypes
    {
        Email = 0,
        Web = 1
    }

    public class PhishingSave
    {
        public int dayLeft;

        public AtkTypes atkType;

        public Target questTarget;

        public int dictIndex;
    }

    public void TriggerSelf()
    {
        int mailIndex = EmailManager.SendPhishingMail();
    }

    public string TriggerNPC(int index)
    {
        //Both mail and web phishing
        Target NPC = (Target) index;
        int rand = Random.Range(0, 1);
        switch (rand)
        {
            case 0:
                return TriggerEmailCase(NPC);
            case 1:
                return TriggerEmailCase(NPC);
            // return TriggerWebCase(NPC);
            default:
                Debug.Log("WTF - random range 0 to 1 and still fail?");
                return "";
        }
    }

    private string TriggerEmailCase(Target NPC)
    {
        string questDetail =
            email_QuestDetail[Random.Range(0, email_QuestDetail.Length - 1)];
        NPCcontroller.TriggerNPCquest(NPC, Scenario.Phishing, questDetail);
        int index = EmailManager.GetRandomPhishingMail();
        phishingSave =
            new PhishingSave {
                dayLeft = 4,
                atkType = AtkTypes.Email,
                questTarget = NPC,
                dictIndex = index
            };
        Debug
            .Log("Phishing triggered, Target: " + NPC + " dictIndex: " + index);
        return JsonUtility.ToJson(phishingSave);
    }

    private string TriggerWebCase(Target NPC)
    {
        string questDetail =
            web_QuestDetail[Random.Range(0, web_QuestDetail.Length - 1)];
        NPCcontroller.TriggerNPCquest(NPC, Scenario.Phishing, questDetail);
        int index = 0;
        phishingSave =
            new PhishingSave {
                dayLeft = 4,
                atkType = AtkTypes.Email,
                questTarget = NPC,
                dictIndex = index
            };
        Debug.Log("Phishing triggered, Target: " + NPC + " dictIndex: ");
        return JsonUtility.ToJson(phishingSave);
    }
}
