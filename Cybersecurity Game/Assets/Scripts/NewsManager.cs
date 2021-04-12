using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewsManager : MonoBehaviour
{
    private static News[] newsDict;

    private News[] newsArray = new News[3];

    [SerializeField] private GameManager gameManager;


    
    void Start()
    {
        SetNewsDictionary();
        gameManager.DayPassed += UpdateNews;
    }

    private void UpdateNews()
    {
        for (int i = 0; i < newsArray.Length; i++)
        {
            int templateLength = newsDict.Length - 1;
            int rIndex = Random.Range(0, templateLength);

            newsArray[i] = newsDict[rIndex];
        }

    }

    //Overload method
    private void SetNews(string scenarioId)
    {
        for (int i = 0; i < newsArray.Length; i++)
        {
            int templateLength = newsDict.Length - 1;
            int rIndex = Random.Range(0, templateLength);

            newsArray[i] = newsDict[rIndex];

            if (i == 3)
            {
                //logic !! find template that match the scenarioId
                foreach(News news in newsDict)
                {
                    if(news.template.Equals(scenarioId))
                    {
                        newsArray[i] = news;
                    }
                }
            }
        }

    }

    private void SetNewsDictionary()
    {
        string jsonString = SaveSystem.LoadDictionary("NewsTemplate.json");
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find EmailTemplate.json");
            Application.Quit();
        }
        newsDict = JsonHelper.FromJson<News>(jsonString);
    }
}

[Serializable]
public class News
{
    public string template;
    public int scenario;
    public string topic;
    public string content;
    public string image;

}
