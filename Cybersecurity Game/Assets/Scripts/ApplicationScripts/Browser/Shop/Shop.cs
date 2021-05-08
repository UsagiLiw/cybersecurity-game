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

    private static bool rememberPwd = false;

    public void OnEnable()
    {
        if (!string.IsNullOrEmpty(PasswordManager.password1))
        {
            OpenShopLogin();
        }
        else
        {
            OpenPage (register);
        }
    }

    public void OpenShopLogin()
    {
        CloseAllChild();
        login.SetActive(true);
        if (!rememberPwd) login_passwordInput.text = "";
    }

    public void VerifyShopLogin()
    {
        GameObject incorrectPasswordText =
            gameObject
                .transform
                .Find("Login")
                .transform
                .Find("IncorrectText")
                .gameObject;

        if (login_passwordInput.text.Equals(PasswordManager.password1))
        {
            incorrectPasswordText.SetActive(false);
            OpenPage (home);
        }
        else
        {
            incorrectPasswordText.SetActive(true);
        }
    }

    public void RegisterShopAccount()
    {
        PasswordManager.EditPassword(1, regis_passwordInput.text);
        OpenShopLogin();
    }

    /* --------- Controller Section ----------*/
    public void ResetPasswordPressed()
    {
        OpenPage (resetPassword);
    }

    public void OpenPage(GameObject pageObject)
    {
        CloseAllChild();
        pageObject.SetActive(true);
    }

    private void CloseAllChild()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void RememberPassword(bool value)
    {
        rememberPwd = value;
    }
}
