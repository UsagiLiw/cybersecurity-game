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

    [SerializeField] private Text cloudPrice;
    [SerializeField] private Text antivirusPrice;
    [SerializeField] private Text trainingPrice;
    [SerializeField] private Text osPrice;

    private void Awake()
    {
        shopManager = GameObject.Find("GameManager").GetComponent<ShopManager>();
        if(shopManager != null)
        {
            Debug.Log("Found shopManager");
        } else Debug.Log("Not Found shopManager");
    }

    private void Start()
    {
        this.cloudPrice.text = "$ " + shopManager.items[0].price;
        this.antivirusPrice.text = "$ " + shopManager.items[1].price;
        this.trainingPrice.text = "$ " + shopManager.items[2].price;
        this.osPrice.text = "$ " + shopManager.items[3].price;
    }

    private void OnEnable()
    {
        UpdateBalance();
        shopManager.ItemExpired += ReEnableButton;
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

    private void ReEnableButton(int itemIndex)
    {
        switch(itemIndex)
        {
            case 0:
                Debug.Log("Cloud Button reactivate");
                cloudButton.interactable = true;
                break;
            case 1:
                Debug.Log("Antivirus Button reactivate");
                antivirusButton.interactable = true;
                break;
            case 2:
                Debug.Log("Training Button reactivate");
                trainingButton.interactable = true;
                break;
            case 3:
                Debug.Log("OS Button reactivate");
                osButton.interactable = true;
                break;
            default:
                Debug.Log("Can't reactivate what button is this ---> " + itemIndex);
                break;
        }
    }

    private void UpdateBalance()
    {
        balanceText.text = BudgetManager.currentBudget.ToString();
    }

    private void OnDisable()
    {
        shopManager.ItemExpired -= ReEnableButton;
    }
}
