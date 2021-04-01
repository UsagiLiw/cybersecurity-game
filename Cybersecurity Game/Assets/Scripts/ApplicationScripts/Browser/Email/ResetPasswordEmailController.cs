using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordEmailController : MonoBehaviour
{
    public Email email;

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
        if (VerifyEmailPassword(oldPassword.text))
        {
            PasswordManager.EditPassword(2, newPassword.text);
            email.OpenPage(email.inbox);
        }
        else incorrectPasswordText.SetActive(true);
    }

    public void BackButtonPressed()
    {
        email.OpenPage(email.inbox);
    }

    private bool VerifyEmailPassword(string password)
    {
        if (password.Equals(PasswordManager.password2))
        {
            Debug.Log("Correct Password!");
            return true;
        }
        else return false;
    }


}
