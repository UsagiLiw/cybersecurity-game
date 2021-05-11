using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    private BudgetManager budgetManager;

    [SerializeField]
    public List<Item> items;

    public delegate void ItemExpireHandler(int itemIndex);

    public event ItemExpireHandler ItemExpired;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy (gameObject);
        }
    }

    private void Start()
    {
        budgetManager = gameObject.GetComponent<BudgetManager>();
        // foreach (Item item in items)
        // {
        //     item.isPurchased = false;
        // }
        GameManager.DayPassed += CountDayForItem;
    }

    public bool BuyItem(int index)
    {
        items[index].isPurchased = true;

        // If true : Continue process , If false : skip
        if (budgetManager.ModifyBudget(-items[index].price))
        {
            switch (index)
            {
                case 0:
                    // Debug.Log("Buy Cloud Storage");
                    break;
                case 1:
                    ComputerManager.Instance.ActivateAntivirus();

                    // Debug.Log("Buy Antivirus");
                    break;
                case 2:
                    // Debug.Log("Buy Training Course");
                    break;
                case 3:
                    // Debug.Log("Buy OS Update");
                    break;
                default:
                    // Debug.Log("Dafuq did you buy?");
                    break;
            }
            GameManager.InvokeSaveData();
            return true;
        }
        else
        {
            return false;
        }
    }

    public string[] SendSaveData()
    {
        string[] saveArr = new string[4];
        for (int i = 0; i < 4; i++)
        {
            Item temp = items[i];
            saveArr[i] = JsonUtility.ToJson(temp);
        }
        return saveArr;
    }

    public void LoadItemData(string[] itemArr)
    {
        if (itemArr.Length != items.Count)
        {
            return;
        }
        for (int i = 0; i < itemArr.Length; i++)
        {
            Item temp = JsonUtility.FromJson<Item>(itemArr[i]);
            items[i].isPurchased = temp.isPurchased;
            items[i].dayPassed = temp.dayPassed;
            if (temp.isPurchased)
            {
                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        ComputerManager.Instance.ActivateAntivirus();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
        }
    }

    //Subscribe to day passed event
    private void CountDayForItem()
    {
        int i = 0;
        foreach (Item item in items)
        {
            if (item.isPurchased) item.dayPassed++;

            if (item.dayPassed == item.expiredDays)
            {
                item.dayPassed = 0;
                switch (i)
                {
                    case 0:
                        break;
                    case 1:
                        ComputerManager.Instance.DeactivateAntivirus();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
                if (ItemExpired != null) ItemExpired.Invoke(i);
            }
            i++;
        }
    }

    public bool CheckItemPurchase(int i)
    {
        if (i >= 0 && i < 4) return items[i].isPurchased;

        return false;
    }
}

[Serializable]
public class Item
{
    public string name;

    public int price;

    public int expiredDays;

    public bool isPurchased;

    public int dayPassed = 0;
}
