using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ransomBehavior : MonoBehaviour
{
    public Button backup_button;

    public Button pay_button;

    public Text pay_text;

    void Start()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(false);
        GameManager.DayPassed += UpdateOptions;
    }

    private void OnDisable()
    {
        GameManager.DayPassed -= UpdateOptions;
        Destroy(this.gameObject);
    }

    public void ActivateChoice()
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        UpdateOptions();
    }

    private void UpdateOptions()
    {
        //Set cloud storage option
        backup_button.interactable = ShopManager.Instance.CheckItemPurchase(0);

        //Set pay ransom option
        int currentBudget = BudgetManager.currentBudget;
        if (currentBudget >= 100000)
            pay_button.interactable = true;
        else
            pay_button.interactable = false;
        pay_text.text =
            "Pay for ransom\n( Cost $100,000 - You have " +
            currentBudget +
            " )";
    }

    public void Choose(int i)
    {
        MalwareController malwareController =
            GameObject
                .Find("ScenarioManager")
                .GetComponent<MalwareController>();
        switch (i)
        {
            case 1:
                malwareController.InvokeScenarioSuccess();
                break;
            case 2:
                BudgetManager.Instance.ModifyBudget(-100000);
                malwareController.InvokeRansomFailure(true);
                break;
            case 3:
                malwareController.InvokeRansomFailure(false);
                break;
            default:
                break;
        }
        this.gameObject.SetActive(false);
    }
}
