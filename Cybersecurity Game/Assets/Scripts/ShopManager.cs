using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private BudgetManager budgetManager;
    private ComputerManager computerManager;

    [SerializeField] List<Item> items;
    private void Start()
    {
        budgetManager = gameObject.GetComponent<BudgetManager>();
        foreach (Item item in items)
        {
            item.isPurchased = false;
        }
    }
    public void BuyItem(int index)
    {
        Debug.Log("Buy item index " + index);
        items[index].isPurchased = true;

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
}

[Serializable]
public class Item
{
    public string name;
    public bool isPurchased; 
    public int price;
}
