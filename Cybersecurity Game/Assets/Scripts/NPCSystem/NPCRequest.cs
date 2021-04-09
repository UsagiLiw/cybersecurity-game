using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCRequest : MonoBehaviour
{
    public GameObject NPC1_businessMan;

    public GameObject NPC2_womanPrehistoric;

    public GameObject NPC3_manNinja;

    public GameObject NPC4_manClown;

    public GameObject detailField;

    public void EnableNPC()
    {
        Scenario currentScenario;
        Target currentTarget;
        string requestDetail;
        (currentScenario, currentTarget, requestDetail) =
            NPCcontroller.GetRequestDetail();
        switch (currentTarget)
        {
            case Target.CEO:
                NPC1_businessMan.SetActive(true);
                break;
            case Target.Employee1:
                NPC3_manNinja.SetActive(true);
                break;
            case Target.Employee2:
                NPC2_womanPrehistoric.SetActive(true);
                break;
            case Target.Meeting:
                NPC1_businessMan.SetActive(true);
                break;
            case Target.IT:
                NPC4_manClown.SetActive(true);
                break;
            default:
                Debug.Log("Warning - target not an exisitng NPC");
                break;
        }

        Text detail = detailField.transform.GetComponent<Text>();
        detail.text = requestDetail;
    }

    public void ClickContinue()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        NPCcontroller npcController = gameManager.GetComponent<NPCcontroller>();
        npcController.CreateNPCScreen();
        Destroy(this.gameObject);
    }
}
