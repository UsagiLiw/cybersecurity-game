﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private BudgetManager budgetManager;
    private ComputerManager computerManager;

    [SerializeField] private List<Item> items;
    [SerializeField] private GameManager gameManager;

    public delegate void ItemExpireHandler(int itemIndex);
    public event ItemExpireHandler ItemExpired;

    private void Start()
    {
        budgetManager = gameObject.GetComponent<BudgetManager>();
        computerManager = gameObject.GetComponent<ComputerManager>();
        foreach (Item item in items)
        {
            item.isPurchased = false;
        }
        gameManager.DayPassed += CountDayForItem;
    }

    public void BuyItem(int index)
    {
        Debug.Log("Buy item index " + index);
        items[index].isPurchased = true;

        // Start count down for each item 

        budgetManager.ModifyBudget(-items[index].price);
        Debug.Log("Budget cut " + items[index].price);

        switch(index)
        {
            case 0:
                Debug.Log("Buy Cloud Storage");
                break;
            case 1:
                computerManager.ActivateAntivirus();
                Debug.Log("Buy Antivirus");
                break;
            case 2:
                Debug.Log("Buy Training Course");
                break;
            case 3:
                Debug.Log("Buy OS Update");
                break;
            default:
                Debug.Log("Dafuq did you buy?");
                break;
        }
    }

    //Subscribe to day passed event
    private void CountDayForItem()
    {
        int i = 0;
        foreach(Item item in items)
        {
            if(item.isPurchased) 
                item.dayPassed++;

            if(item.dayPassed == 5)
            {
                item.dayPassed = 0;
                ItemExpired.Invoke(i);
            }
            i++;
        }
    }

}

[Serializable]
public class Item
{
    public string name;
    public bool isPurchased; 
    public int price;
    public int dayPassed = 0;
}