using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHomeController : MonoBehaviour
{
    public Text balanceText;

    private ShopManager shopManager;

    private void OnEnable()
    {
        UpdateBalance();
    }

    private void Start()
    {
        shopManager = GameObject.Find("GameManager").GetComponent<ShopManager>();
        if(shopManager != null)
        {
            Debug.Log("Found shopManager");
        } else Debug.Log("Not Found shopManager");
    }

    public void BuyButtonClicked(int index)
    {
        shopManager.BuyItem(index);
    }

    private void UpdateBalance()
    {
        balanceText.text = BudgetManager.currentBudget.ToString();
    }
}
