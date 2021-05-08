using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHomeController : MonoBehaviour
{
    public Text balanceText;

    private ShopManager shopManager;

    [SerializeField]
    private Button cloudButton;

    [SerializeField]
    private Button antivirusButton;

    [SerializeField]
    private Button trainingButton;

    [SerializeField]
    private Button osButton;

    [SerializeField]
    private Text cloudPrice;

    [SerializeField]
    private Text antivirusPrice;

    [SerializeField]
    private Text trainingPrice;

    [SerializeField]
    private Text osPrice;

    private void Awake()
    {
        shopManager =
            GameObject.Find("GameManager").GetComponent<ShopManager>();
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
        CheckPurchase();
        shopManager.ItemExpired += ReEnableButton;
    }

    public void BuyButtonClicked(int index)
    {
        if (shopManager.BuyItem(index))
        {
            switch (index)
            {
                case 0:
                    cloudButton.interactable = false;
                    break;
                case 1:
                    antivirusButton.interactable = false;
                    break;
                case 2:
                    trainingButton.interactable = false;
                    break;
                case 3:
                    osButton.interactable = false;
                    break;
                default:
                    Debug.Log("No button to disable interaction");
                    break;
            }
        }
        UpdateBalance();
    }

    private void ReEnableButton(int itemIndex)
    {
        switch (itemIndex)
        {
            case 0:
                cloudButton.interactable = true;
                break;
            case 1:
                antivirusButton.interactable = true;
                break;
            case 2:
                trainingButton.interactable = true;
                break;
            case 3:
                osButton.interactable = true;
                break;
            default:
                Debug
                    .Log("Can't reactivate what button is this ---> " +
                    itemIndex);
                break;
        }
    }

    private void CheckPurchase()
    {
        cloudButton.interactable = !shopManager.CheckItemPurchase(0);

        antivirusButton.interactable = !shopManager.CheckItemPurchase(1);

        trainingButton.interactable = !shopManager.CheckItemPurchase(2);

        osButton.interactable = !shopManager.CheckItemPurchase(3);
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
