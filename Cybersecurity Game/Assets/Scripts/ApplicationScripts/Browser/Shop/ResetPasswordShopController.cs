using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordShopController : MonoBehaviour
{
    public Shop shop;

    [SerializeField] private InputField oldPassword;
    [SerializeField] private InputField newPassword;

    [SerializeField] private GameObject incorrectPasswordText;

    private void OnEnable()
    {
        oldPassword.text = "";
        newPassword.text = "";
        incorrectPasswordText.SetActive(false);
    }

    public void EditShopPassword()
    {
        if (VerifyShopPassword(oldPassword.text))
        {
            PasswordManager.EditPassword(1, newPassword.text);
            shop.OpenPage(shop.home);
        }
        else incorrectPasswordText.SetActive(true);
    }

    public void BackButtonPressed()
    {
        shop.OpenPage(shop.home);
    }

    private bool VerifyShopPassword(string password)
    {
        if (password.Equals(PasswordManager.password1))
        {
            Debug.Log("Correct Password!");
            return true;
        }
        else return false;
    }


}
