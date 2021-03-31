using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager : MonoBehaviour
{
    public static int currentReputation;

    public void ModifyReputation(int amt)
    {
        currentReputation += amt;
        if (currentReputation > 100)
        {
            currentReputation = 100;
            Debug.Log("Reputation exceed 100. Set back to 100.");
        }
        if (currentReputation <= 0)
        {
            currentReputation = 0;
            Debug.Log("Reputation depleted, you lost");
        }
    }

    public void SetCurrentRep(int rep)
    {
        currentReputation = rep;
    }
}
