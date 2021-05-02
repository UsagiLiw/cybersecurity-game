﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Browser : MonoBehaviour
{
    public GameObject homePage;

    public GameObject shop;

    public GameObject email;

    public GameObject news;

    public GameObject siteInfo;

    public GameObject linkHover;

    public GameObject bar;

    public GameObject ads;

    public Text address;

    private static string shopAddress = "www.anaconda.com";

    private static string emailAddress = "www.coldmail.com";

    private static string newsAddress = "www.newestnews.com";

    private void OnEnable()
    {
        address.text = "";
        homePage.SetActive(true);
        bar.SetActive(true);
        shop.SetActive(false);
        email.SetActive(false);
        news.SetActive(false);
        siteInfo.SetActive(false);
        linkHover.SetActive(false);
        int malwareType = ComputerManager.Instance.CheckActiveComMalwareType();
        if (malwareType == (int) MalwareType.Adware)
            ads.SetActive(true);
        else
            ads.SetActive(false);
    }

    public void CloseBrowser()
    {
        gameObject.SetActive(false);
    }

    public void OpenShopLogin()
    {
        ads.SetActive(false);
        address.text = shopAddress;
        homePage.SetActive(false);
        email.SetActive(false);
        news.SetActive(false);

        shop.SetActive(true);
    }

    public void OpenEmail()
    {
        ads.SetActive(false);
        address.text = emailAddress;
        homePage.SetActive(false);
        shop.SetActive(false);
        news.SetActive(false);

        email.SetActive(true);
    }

    public void OpenNews()
    {
        ads.SetActive(false);
        address.text = newsAddress;
        homePage.SetActive(false);
        news.SetActive(true);
    }

    public void GoHome()
    {
        address.text = "";
        homePage.SetActive(true);
        shop.SetActive(false);
        news.SetActive(false);
        email.SetActive(false);
        bar.SetActive(true);
        int malwareType = ComputerManager.Instance.CheckActiveComMalwareType();
        if (malwareType == (int) MalwareType.Adware)
            ads.SetActive(true);
        else
            ads.SetActive(false);
    }

    public void ShowSiteInfo()
    {
        siteInfo.SetActive(true);
    }
}
