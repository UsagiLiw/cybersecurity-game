using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingController : MonoBehaviour
{
    public static PhishingSave phishingSave;

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

        public int scenarioIndex;
    }

    public void TriggerSelf()
    {
        int mailIndex = EmailManager.SendPhishingMail();
    }

    public string TriggerNPC(int index)
    {
        //Both mail and web phishing
        int rand = Random.Range(0, 1);
        switch (rand)
        {
            case 0:
                return TriggerEmailCase();
            case 1:
                return TriggerWebCase();
            default:
                Debug.Log("WTF - random range 0 to 1 and still fail?");
                return "";
        }
    }

    private string TriggerEmailCase()
    {
        return "Boi";
    }

    private string TriggerWebCase()
    {
        return "webBOi";
    }
}
