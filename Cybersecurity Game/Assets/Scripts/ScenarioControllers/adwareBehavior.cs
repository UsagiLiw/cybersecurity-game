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

    private static string adsImage_Path = "AdsDesktop/";

    private void OnEnable()
    {
        int index = Random.Range(0, desktopAds.Length);

        notiBox.SetActive(false);
        notiImage.SetActive(false);

        if (string.IsNullOrEmpty(desktopAds[index].adsFile))
        {
            ShowNotiBox(desktopAds[index]);
        }
        else
        {
            ShowNotiImage(desktopAds[index]);
        }
    }

    private void ShowNotiBox(DesktopAdsClass currentAds)
    {
        notiBox.SetActive(true);

        Text topic = notiBox.transform.GetChild(1).gameObject.transform.GetComponent<Text>();
        Text content = notiBox.transform.GetChild(2).gameObject.transform.GetComponent<Text>();
        topic.text = currentAds.adsHeader;
        content.text = currentAds.adsDetail;
    }

    private void ShowNotiImage(DesktopAdsClass currentAds)
    {
        notiImage.SetActive(true);

        Text topic = notiImage.transform.GetChild(1).gameObject.transform.GetComponent<Text>();
        Image content = notiBox.transform.GetChild(2).gameObject.transform.GetComponent<Image>();
        topic.text = currentAds.adsHeader;
        content.sprite = Resources.Load<Sprite>(adsImage_Path + currentAds.adsFile);
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
