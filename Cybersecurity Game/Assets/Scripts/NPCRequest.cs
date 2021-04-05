using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRequest : MonoBehaviour
{
    public GameObject NPC1_businessMan;

    public GameObject NPC2_womanPrehistoric;

    public GameObject NPC3_manNinja;

    public GameObject NPC4_manClown;

    private void OnEnable()
    {
    }

    public void EnableNPC(Target target_NPC)
    {
        switch (target_NPC)
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
    }
}
