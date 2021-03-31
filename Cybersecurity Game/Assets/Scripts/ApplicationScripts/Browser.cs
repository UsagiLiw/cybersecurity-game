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

    public GameObject gameManager;

    private void OnEnable()
    {
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
        // GameObject.Find("Browser").SetActive(false);
        gameObject.SetActive(false);
    }

    public void OpenShopLogin()
    {
        homePage.SetActive(false);
        email.SetActive(false);
        news.SetActive(false);

        shop.SetActive(true);
    }

    public void OpenEmail()
    {
        homePage.SetActive(false);
        shop.SetActive(false);
        news.SetActive(false);

        email.SetActive(true);
    }

    public void OpenNews()
    {
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
}
