using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsController : MonoBehaviour
{
    [SerializeField] private NewsObject[] normalContents;
    [SerializeField] private NewsObject scenarioContent;

    private NewsManager newsManager;
    private GameManager gameManager;

    private void Awake()
    {
        newsManager = GameObject.Find("GameManager").GetComponent<NewsManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnEnable()
    {
        UpdateNewsPanel();
    }

    void Start()
    {
        gameManager.DayPassed += UpdateNewsPanel;
    }

    private void UpdateNewsPanel ()
    {
        News[] arNews = newsManager.newsArray;
        for(int i = 0; i < normalContents.Length; i++)
        {
            normalContents[i].topic.text = arNews[i].topic;
            normalContents[i].content.text = arNews[i].content;
            normalContents[i].image.sprite = Resources.Load<Sprite>(arNews[i].image);
        }
        scenarioContent.topic.text = arNews[arNews.Length - 1].topic;
        scenarioContent.content.text = arNews[arNews.Length - 1].content;
        scenarioContent.image.sprite = Resources.Load<Sprite>(arNews[arNews.Length - 1].image);
    }
}

[Serializable]
public class NewsObject
{
    [SerializeField] public Text topic;
    [SerializeField] public Text content;
    [SerializeField] public Image image;
}
