using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCComputer : MonoBehaviour
{
    private PhishingSave phishingDetail;

    private void OnEnable()
    {
        Debug.Log("I am alive now");
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        this.transform.GetChild(6).gameObject.SetActive(true);
        this.transform.GetChild(7).gameObject.SetActive(true);
        phishingDetail = PhishingController.GetPhishingDetail();
    }

    private void EnablePhishingTarget()
    {
        switch (phishingDetail.atkType)
        {
            case AtkTypes.Email:
                TriggerEmailScenario();
                break;
            case AtkTypes.Web:
                TriggerWebScenario(phishingDetail.dictIndex);
                break;
        }
    }

    private void TriggerEmailScenario()
    {
        GameObject phishingEmail = this.transform.GetChild(0).gameObject;
        phishingEmail.SetActive(true);
    }

    private void TriggerWebScenario(int webIndex)
    {
    }
}
