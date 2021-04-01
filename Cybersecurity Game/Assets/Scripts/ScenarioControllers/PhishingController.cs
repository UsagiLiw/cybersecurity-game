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
        return "boi";
    }
}
