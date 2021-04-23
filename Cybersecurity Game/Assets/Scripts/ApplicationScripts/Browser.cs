using System.Collections;
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
    }

    public void CloseBrowser()
    {
        gameObject.SetActive(false);
    }

    public void OpenShopLogin()
    {
        address.text = shopAddress;
        homePage.SetActive(false);
        email.SetActive(false);
        news.SetActive(false);

        shop.SetActive(true);
    }

    public void OpenEmail()
    {
        address.text = emailAddress;
        homePage.SetActive(false);
        shop.SetActive(false);
        news.SetActive(false);

        email.SetActive(true);
    }

    public void OpenNews()
    {
        address.text = newsAddress;
        homePage.SetActive(false);
        news.SetActive(true);
    }

    public void GoHome()
    {
        homePage.SetActive(true);
        shop.SetActive(false);
        news.SetActive(false);
        email.SetActive(false);
        bar.SetActive(true);
    }

    public void ShowSiteInfo()
    {
        siteInfo.SetActive(true);
    }
}
