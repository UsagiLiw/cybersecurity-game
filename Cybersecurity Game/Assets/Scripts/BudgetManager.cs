using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetManager : MonoBehaviour
{
    public static BudgetManager Instance { get; private set; }

    public static int currentBudget;

    public static int income;

    public int[] incomePerDay;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.DayPassed += NewDayHasCome;
        ReputationManager.ReputationChanged += CheckIncome;
        CheckIncome();
    }

    private void CheckIncome()
    {
        int value = ReputationManager.currentReputation;
        if (value >= 0 && value < 25)
            income = incomePerDay[0];
        else if (value >= 25 && value < 50)
            income = incomePerDay[1];
        else if (value >= 50 && value < 75)
            income = incomePerDay[2];
        else if (value >= 75 && value <= 100)
            income = incomePerDay[3];
        else
        {
            Debug.Log("Error - Something went wrong with rep value");
            income = incomePerDay[0];
        }
    }

    private void NewDayHasCome()
    {
        currentBudget += income;
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
            currentBudget = 0;
            return false;
        }
        currentBudget = amount;
        return true;
    }
}
