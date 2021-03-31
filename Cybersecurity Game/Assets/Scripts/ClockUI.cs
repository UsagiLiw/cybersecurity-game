using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    public static float currentTimer;

    public static float dayTime;

    public Text currentDay;

    public Image timerBar;

    private void Start()
    {
        dayTime = GameManager.dayTime;
    }

    void Update()
    {
        currentTimer = GameManager.currentTimer;
        timerBar.fillAmount = currentTimer / dayTime;
        currentDay.text = "Day " + GameManager.days;
    }
}
