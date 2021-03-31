using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Text currentBudget;

    public Text currentRep;

    public GameObject repIcon;

    public Sprite greenSprite;

    public Sprite yellowSprite;

    public Sprite orangeSprite;

    public Sprite redSprite;

    private void Start()
    {
        int repValue = ReputationManager.currentReputation;
        currentBudget.text = BudgetManager.currentBudget.ToString();
        currentRep.text = repValue.ToString() + " / 100";
        ChangeRepIcon (repValue);
    }

    // Update is called once per frame
    void Update()
    {
        int repValue = ReputationManager.currentReputation;

        currentBudget.text = BudgetManager.currentBudget.ToString();
        currentRep.text = repValue.ToString() + " / 100";
        StartCoroutine(IconUpdateWaiter(repValue));
    }

    IEnumerator IconUpdateWaiter(int value)
    {
        yield return new WaitForSeconds(5);
        ChangeRepIcon (value);
    }

    void ChangeRepIcon(int value)
    {
        if (value >= 0 && value < 25)
        {
            repIcon.GetComponent<Image>().sprite = redSprite;
        }
        else if (value >= 25 && value < 50)
        {
            repIcon.GetComponent<Image>().sprite = orangeSprite;
        }
        else if (value >= 50 && value < 75)
        {
            repIcon.GetComponent<Image>().sprite = yellowSprite;
        }
        else if (value >= 75 && value <= 100)
        {
            repIcon.GetComponent<Image>().sprite = greenSprite;
        }
        else
        {
            Debug.Log("Error - Something went wrong with rep value");
            repIcon.GetComponent<Image>().sprite = redSprite;
        }
    }
}
