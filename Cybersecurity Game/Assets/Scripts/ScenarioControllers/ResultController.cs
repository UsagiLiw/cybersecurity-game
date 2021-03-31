using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultController : MonoBehaviour
{
    public static int pwdAtk_increase;

    public static int pwdAtk_decrease;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            Debug.Log("Resume Game");
        }
    }

    public static void ShowSuccess(string detail, scenario currentScenario)
    {
         Debug.Log("IN SHOWSUCCESS: " + detail);
        Debug.Log("IN SHOWSUCCESS: " + currentScenario);
        Time.timeScale = 0f;
    }

    public static void ShowFailed(string detail, scenario currentScenario)
    {
        Debug.Log("IN SHOWFAILED: " + detail);
        Debug.Log("IN SHOWFAILED: " + currentScenario);
        Time.timeScale = 0f;
    }
}
