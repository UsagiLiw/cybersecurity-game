using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    public static string password1;

    public static string password2;

    public delegate void PasswordChangeAction();

    public static event PasswordChangeAction PasswordHasChanged;

    public static void EditPassword(int num, string newPassword)
    {
        switch (num)
        {
            case 1:
                password1 = newPassword;
                if (PasswordHasChanged != null)
                {
                    PasswordHasChanged.Invoke();
                    Debug.Log("Changed shop password");
                }
                break;
            case 2:
                password2 = newPassword;
                if (PasswordHasChanged != null)
                {
                    PasswordHasChanged.Invoke();
                    Debug.Log("Changed email password");
                }
                break;
            default:
                Debug.Log("Error - Try to edit non-existing password");
                break;
        }
    }

    public void SetAllPasswords(string pwd1, string pwd2)
    {
        password1 = pwd1;
        password2 = pwd2;
    }

    public bool CheckPassword(int num, string pwd)
    {
        switch (num)
        {
            case 1:
                if (password1 == pwd)
                {
                    return true;
                }
                break;
            case 2:
                if (password2 == pwd)
                {
                    return true;
                }
                break;
            default:
                Debug.Log("Error - Try to edit non-existing password");
                break;
        }
        return false;
    }

    public static string[] ShowPasswords()
    {
        return new string[] { password1, password2 };
    }
}
