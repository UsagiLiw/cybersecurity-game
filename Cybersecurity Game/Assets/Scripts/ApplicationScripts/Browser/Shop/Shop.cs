using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public InputField login_passwordInput;
    public InputField regis_passwordInput;

    public GameObject home;
    public GameObject login;
    public GameObject register;
    public GameObject resetPassword;

    public void OnEnable()
    {
        if (PasswordManager.password1 != null)
        {
            OpenPage(login);
            Debug.Log("to shop login page");
        }
        else
        {
            Debug.Log("To shop register page");
            RegisterShop();
        }
    }

    public void VerifyShopLogin()
    {
        GameObject incorrectPasswordText = gameObject.transform.Find("Login").transform.Find("IncorrectText").gameObject;

        if (login_passwordInput.text.Equals(PasswordManager.password1))
        {
            Debug.Log("Correct Password!");
            incorrectPasswordText.SetActive(false);
            OpenPage(home);
        }
        else
        {
            Debug.Log("Wrong Password!");
            incorrectPasswordText.SetActive(true);
        }
    }
    private void RegisterShop()
    {
        OpenPage(register);
    }

    /* --------- Controller Section ----------*/
    public void ResetPasswordPressed()
    {
        OpenPage(resetPassword);
    }

    public void OpenPage(GameObject pageObject)
    {
        CloseAllChild();
        pageObject.SetActive(true);
    }

    private void CloseAllChild()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
            Debug.Log(child);
        }
    }
    // private void OpenShop()
    // {
    //     string stringInput = "bruh";

    //     homePage.SetActive(false);
    //     if (passwordManager.CheckPassword(1, stringInput))
    //     {
    //         shop.SetActive(true);
    //     }
    //     else
    //     {
    //         Debug.Log("Wrong password");
    //         //do something here
    //     }
    // }
}
