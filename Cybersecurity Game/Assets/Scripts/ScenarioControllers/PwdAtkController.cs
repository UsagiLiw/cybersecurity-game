using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

using Random = UnityEngine.Random;

public class PwdAtkController : MonoBehaviour
{
    public static int dayLeft;

    public static Accounts target;

    public static PasswordScore complexity;

    private static string shopPassword;

    private static string emailPassword;

    private void Start()
    {
        shopPassword = PasswordManager.password1;
        emailPassword = PasswordManager.password2;
    }

    public string CheckVulnerability()
    {
        shopPassword = PasswordManager.password1;
        emailPassword = PasswordManager.password2;
        bool secure = false;
        Accounts attacking;

        PasswordScore shopScore = CheckStrength(shopPassword);
        PasswordScore emailScore = CheckStrength(emailPassword);

        //If both account use same password, attack both
        if (shopPassword.Equals(emailPassword))
        {
            secure = GetChance(emailScore);
            attacking = Accounts.all;
            complexity = emailScore;
        }
        else
        {
            if (shopScore >= emailScore)
            {
                secure = GetChance(emailScore);
                attacking = Accounts.email;
                complexity = shopScore;
            }
            else
            {
                secure = GetChance(shopScore);
                attacking = Accounts.shop;
                complexity = emailScore;
            }
        }
        PasswordManager.PasswordHasChanged += ScenarioSuccess; //Subscribe to event

        //if secure, give chance to reset password, if not scenario fail in a day
        if (secure)
        {
            return ScenarioTrigger(attacking, 3);
        }
        else
        {
            return ScenarioTrigger(attacking, 1);
        }
    }

    public (bool, string) UpdateScenarioState()
    {
        dayLeft--;
        if (dayLeft <= 0)
        {
            //Trigger Scenario failure
            return (false, ScenarioFailed());
        }
        PwdAtkObject saveObject =
            new PwdAtkObject {
                dayLeft = dayLeft,
                target = target,
                complexity = complexity
            };

        //Scenario yet to fail
        return (true, JsonUtility.ToJson(saveObject));
    }

    public void SetPasswordScenarioState(string detail)
    {
        PwdAtkObject saveObject = JsonUtility.FromJson<PwdAtkObject>(detail);
        dayLeft = saveObject.dayLeft;
        target = saveObject.target;
        complexity = saveObject.complexity;
        PasswordManager.PasswordHasChanged += ScenarioSuccess;
        NotificationManager
            .SetNewNotification(new Notification("Email",
                "Did you just try to login?"));
    }

    //Return false when forcing scenario fail, return true to start scenario
    private bool GetChance(PasswordScore pwdScore)
    {
        switch (pwdScore)
        {
            case PasswordScore.Blank:
                return false;
            case PasswordScore.VeryWeak:
                return false;
            case PasswordScore.Weak:
                return false;
            case PasswordScore.Medium:
                return true;
            case PasswordScore.Strong:
                return true;
            case PasswordScore.VeryStrong:
                return true;
            default:
                throw new InvalidOperationException("Couldn't process operation: " +
                    pwdScore);
        }
    }

    private string ScenarioTrigger(Accounts account, int time)
    {
        dayLeft = time;
        target = account;
        switch (account)
        {
            case Accounts.all:
                EmailManager.SendScenarioMail(0);
                EmailManager.SendScenarioMail(1);
                break;
            case Accounts.shop:
                EmailManager.SendScenarioMail(1);
                break;
            case Accounts.email:
                EmailManager.SendScenarioMail(0);
                break;
        }
        PwdAtkObject saveObject =
            new PwdAtkObject {
                dayLeft = dayLeft,
                target = target,
                complexity = complexity
            };
        NotificationManager
            .SetNewNotification(new Notification("Email",
                "Did you just try to login?"));
        return JsonUtility.ToJson(saveObject);
    }

    private string ScenarioFailed()
    {
        PasswordManager.PasswordHasChanged -= ScenarioSuccess;
        PwdAtkObject saveObject =
            new PwdAtkObject {
                dayLeft = dayLeft,
                target = target,
                complexity = complexity
            };
        return JsonUtility.ToJson(saveObject);
    }

    private void ScenarioSuccess()
    {
        if (!CheckSuccessCondition())
        {
            return;
        }

        PasswordManager.PasswordHasChanged -= ScenarioSuccess;
        PwdAtkObject saveObject =
            new PwdAtkObject {
                dayLeft = dayLeft,
                target = target,
                complexity = complexity
            };
        string saveString = JsonUtility.ToJson(saveObject);
        ScenarioManager.InvokeScenarioSuccess (saveString);
    }

    private bool CheckSuccessCondition()
    {
        string currentShopPwd = PasswordManager.password1;
        string currentEmailPwd = PasswordManager.password2;
        switch (target)
        {
            case Accounts.all:
                if (
                    !shopPassword.Equals(currentShopPwd) &&
                    !emailPassword.Equals(currentEmailPwd)
                )
                {
                    return true;
                }
                break;
            case Accounts.shop:
                if (!shopPassword.Equals(currentShopPwd))
                {
                    return true;
                }
                break;
            case Accounts.email:
                if (!emailPassword.Equals(currentEmailPwd))
                {
                    return true;
                }
                break;
        }
        return false;
    }

    private static PasswordScore CheckStrength(string password)
    {
        int score = 1;

        if (password.Length < 1) return PasswordScore.Blank;
        if (password.Length < 4) return PasswordScore.VeryWeak;

        if (password.Length >= 8) score++;
        if (password.Length >= 12) score++;
        if (Regex.Match(password, @"/\d+/", RegexOptions.ECMAScript).Success)
            score++;
        if (
            Regex
                .Match(password, @"/[a-z]/", RegexOptions.ECMAScript)
                .Success &&
            Regex.Match(password, @"/[A-Z]/", RegexOptions.ECMAScript).Success
        ) score++;
        if (
            Regex
                .Match(password,
                @"/.[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]/",
                RegexOptions.ECMAScript)
                .Success
        ) score++;

        return (PasswordScore) score;
    }
}

public class PwdAtkObject
{
    public int dayLeft;

    public Accounts target;

    public PasswordScore complexity;
}

public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5
}

public enum Accounts
{
    all = 0,
    shop = 1,
    email = 2
}
