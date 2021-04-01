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

    public string PhishingTrigger(int chosenTarget)
    {
        if (chosenTarget == 0)
        {
            TriggerSelf();
        }
        else
        {
            TriggerNPC();
        }
        return "bruh";
    }

    private void TriggerSelf()
    {
        int mailIndex = EmailManager.SendPhishingMail();
    }

    private void TriggerNPC()
    {
        //Both mail and web phishing
    }
}
