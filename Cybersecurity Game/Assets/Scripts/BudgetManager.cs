using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetManager : MonoBehaviour
{
    public static BudgetManager Instance { get; private set; }

    public static int currentBudget;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool ModifyBudget(int amount)
    {
        int tempAmount = currentBudget;
        tempAmount += amount;
        if (tempAmount < 0)
        {
            return false;
        }
        else
        {
            currentBudget = tempAmount;
            return true;
        }
    }

    public bool SetCurrentBudget(int amount)
    {
        if (amount < 0)
        {
            Debug.Log("Budget value cannot goes below 0");
            return false;
        }
        currentBudget = amount;
        return true;
    }
}
