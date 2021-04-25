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
        Target NPC = (Target) index;
        bool isPhishing = Random.Range(0, 2) == 1 ? true : false;
        if (Random.value > 0.5f)
            return TriggerEmailCase(NPC, isPhishing);
        else
            return TriggerWebCase(NPC, isPhishing);
    }

    public (bool, string) UpdateScenarioState()
    {
        phishingSave.dayLeft--;
        if (phishingSave.dayLeft <= 0)
        {
            return (false, JsonUtility.ToJson(phishingSave));
        }
        return (true, JsonUtility.ToJson(phishingSave));
    }

    public void SetPhishingScenarioState(string detail)
    {
        phishingSave = JsonUtility.FromJson<PhishingSave>(detail);
        string questDetail =
            email_QuestDetail[Random.Range(0, email_QuestDetail.Length - 1)];
        NPCcontroller
            .TriggerNPCQuest(phishingSave.questTarget,
            questDetail,
            Scenario.Phishing);
        Debug
            .Log("Continue phishing: " +
            phishingSave.atkType +
            " Target: " +
            phishingSave.questTarget +
            " dayLeft:" +
            phishingSave.dayLeft);
    }

    private string TriggerEmailCase(Target NPC, bool isPhishing)
    {
        string questDetail =
            email_QuestDetail[Random.Range(0, email_QuestDetail.Length - 1)];
        NPCcontroller.TriggerNPCQuest(NPC, questDetail, Scenario.Phishing);
        int index = 0;
        if (isPhishing)
            index = EmailManager.GetRandomPhishingMail();
        else
            index = EmailManager.GetRandomNormalMail();
        phishingSave =
            new PhishingSave {
                dayLeft = 3,
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
        NPCcontroller.TriggerNPCQuest(NPC, questDetail, Scenario.Phishing);
        int index = Random.Range(1, 4);
        phishingSave =
            new PhishingSave {
                dayLeft = 3,
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

    public static void CheckScenarioCondition(bool legit)
    {
        string saveString = JsonUtility.ToJson(phishingSave);
        NPCcontroller.DisableAllNPC();
        if (phishingSave.isPhishing == legit)
        {
            Debug.Log("success");
            ScenarioManager.InvokeScenarioSuccess (saveString);
        }
        else
        {
            Debug.Log("fail");
            ScenarioManager.InvokeScenarioFailed (saveString);
        }
    }

    public static void InvokeScenarioFailure(bool isPlayer)
    {
        PhishingSave detail = null;
        if (isPlayer)
        {
            detail =
                new PhishingSave {
                    dayLeft = 0,
                    atkType = AtkTypes.Email,
                    questTarget = Target.You,
                    dictIndex = -1,
                    isPhishing = true
                };
        }
        else
        {
            detail = phishingSave;
        }
        ScenarioManager.InvokeScenarioFailed(JsonUtility.ToJson(detail));
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
