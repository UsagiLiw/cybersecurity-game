using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHomeController : MonoBehaviour
{
    public Text balanceText;

    private ShopManager shopManager;

    [SerializeField] private Button cloudButton;
    [SerializeField] private Button antivirusButton;
    [SerializeField] private Button trainingButton;
    [SerializeField] private Button osButton;

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
        UpdateBalance();
        switch(index)
        {
            case 0:
                Debug.Log("buy cloud");
                cloudButton.interactable = false;
                break;
            case 1:
                Debug.Log("buy antivirus");
                antivirusButton.interactable = false;
                break;
            case 2:
                Debug.Log("buy training");
                trainingButton.interactable = false;
                break;
            case 3:
                Debug.Log("buy os update");
                osButton.interactable = false;
                break;
            default:
                Debug.Log("No button to disable interaction");
                break;
        }
    }

    private void UpdateBalance()
    {
        balanceText.text = BudgetManager.currentBudget.ToString();
    }
}
