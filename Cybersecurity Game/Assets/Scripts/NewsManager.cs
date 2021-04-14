using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NewsManager : MonoBehaviour
{
    private List<News> normalNewsDict = new List<News>();
    private List<News> scenarioNewsDict = new List<News>();

    public News[] newsArray = new News[3];

    [SerializeField] private GameManager gameManager;
    
    void Start()
    {
        News[] temp = SetNewsDictionary();
        foreach (News news in temp)
        {
            if (news.scenario == -1) { normalNewsDict.Add(news); }
            else if(news.scenario == -1) { scenarioNewsDict.Add(news); }
        }
        gameManager.DayPassed += UpdateNews;
    }

    private void UpdateNews()
    {
        Scenario scenario = ScenarioManager.onGoingScenario;
        if (scenario == Scenario.None)
        {
            SetNews();
        }
        else
        {
            SetNews((int)scenario);
        }
    }

    private void SetNews()
    {
        for (int i = 0; i < newsArray.Length - 1; i++)
        {
            int rIndex = Random.Range(0, normalNewsDict.Count);
            newsArray[i] = normalNewsDict[rIndex];
        }
    }
    private void SetNews(int scenarioType)
    {
        for (int i = 0; i < newsArray.Length -1 ; i++)
        {
            int rIndex = Random.Range(0, normalNewsDict.Count);
            newsArray[i] = normalNewsDict[rIndex];
            if (i == 2)
            {
                //logic !! find template that match the scenarioType
                foreach(News news in scenarioNewsDict)
                {
                    if(news.scenario.Equals(scenarioType))
                    {
                        newsArray[i] = news;
                    }
                }
            }
        }
    }

    private News[] SetNewsDictionary()
    {
        string jsonString = SaveSystem.LoadDictionary("NewsTemplate.json");
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find EmailTemplate.json");
            Application.Quit();
        }
        return JsonHelper.FromJson<News>(jsonString);
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
