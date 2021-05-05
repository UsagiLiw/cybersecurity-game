using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class adwareBehavior : MonoBehaviour
{
    public DesktopAdsClass[] desktopAds;

    public GameObject notiBox;

    public GameObject notiImage;

    public GameObject notiMiddle;

    private static string adsImage_Path = "AdsDesktop/";

    private void OnEnable()
    {
        int index = Random.Range(0, desktopAds.Length - 1);

        notiBox.SetActive(false);
        notiImage.SetActive(false);
        notiMiddle.SetActive(false);

        if (string.IsNullOrEmpty(desktopAds[index].adsFile))
        {
            ShowNotiBox(desktopAds[index]);
        }
        else
        {
            if (Random.value > 0.5f)
                ShowNotiImage(desktopAds[index]);
            else
                ShowBigImage(desktopAds[index]);
        }
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }

    private void ShowNotiBox(DesktopAdsClass currentAds)
    {
        notiBox.SetActive(true);

        Text topic =
            notiBox
                .transform
                .GetChild(1)
                .gameObject
                .transform
                .GetComponent<Text>();
        Text content =
            notiBox
                .transform
                .GetChild(2)
                .gameObject
                .transform
                .GetComponent<Text>();
        topic.text = currentAds.adsHeader;
        content.text = currentAds.adsDetail;
    }

    private void ShowNotiImage(DesktopAdsClass currentAds)
    {
        notiImage.SetActive(true);

        Text topic =
            notiImage
                .transform
                .GetChild(1)
                .gameObject
                .transform
                .GetComponent<Text>();
        Image content =
            notiImage
                .transform
                .GetChild(2)
                .gameObject
                .transform
                .GetComponent<Image>();
        topic.text = currentAds.adsHeader;
        var adsImage =
            Resources.Load<Sprite>(adsImage_Path + currentAds.adsFile);
        content.sprite = adsImage;
    }

    private void ShowBigImage(DesktopAdsClass currentAds)
    {
        notiMiddle.SetActive(true);
        Image content =
            notiMiddle
                .transform
                .GetChild(1)
                .gameObject
                .transform
                .GetComponent<Image>();
        var adsImage =
            Resources.Load<Sprite>(adsImage_Path + currentAds.adsFile);
        content.sprite = adsImage;
    }

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
}

[System.Serializable]
public class DesktopAdsClass
{
    public string adsHeader;

    public string adsDetail;

    public string adsFile;
}
