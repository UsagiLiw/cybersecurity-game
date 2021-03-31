using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHomeController : MonoBehaviour
{
    public Text balanceText;

    private void OnEnable()
    {
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        balanceText.text = BudgetManager.currentBudget.ToString();
    }
}
