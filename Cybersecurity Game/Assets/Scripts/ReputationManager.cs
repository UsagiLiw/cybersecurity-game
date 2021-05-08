using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager : MonoBehaviour
{
    public static ReputationManager Instance { get; private set; }

    public static int currentReputation;

    public delegate void ReputationAction();

    public static event ReputationAction ReputationChanged;

    public GameObject endGame_Prefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public int ModifyReputation(int amt, float multiplier)
    {
        currentReputation += (int)(amt * multiplier);
        if (currentReputation > 100)
        {
            currentReputation = 100;
        }
        if (currentReputation <= 0)
        {
            currentReputation = 0;
            Debug.Log("Reputation depleted, you lost");
            TriggerGameOver();
        }
        ReputationChanged?.Invoke();
        return currentReputation;
    }

    public void SetCurrentRep(int rep)
    {
        currentReputation = rep;
        if (currentReputation > 100)
        {
            currentReputation = 100;
        }
        if (currentReputation <= 0)
        {
            currentReputation = 0;
            Debug.Log("Reputation depleted, you lost");
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        GameObject gameOver = Instantiate(endGame_Prefab) as GameObject;
        gameOver.transform.SetParent(GameObject.Find("GUI").transform);
        gameOver.transform.SetAsLastSibling();
    }
}
