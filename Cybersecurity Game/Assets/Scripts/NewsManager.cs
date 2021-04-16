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
    
    void Awake()
    {
        News[] temp = SetNewsDictionary();
        foreach (News news in temp)
        {
            if (news.scenario == -1) { 
                normalNewsDict.Add(news);
                //Debug.Log("Normal " + news.template + " " + news.scenario);
            }
            else if(news.scenario != -1) { 
                scenarioNewsDict.Add(news);
                Debug.Log("Scenario" + news.template + " " + news.scenario);
            }
        }
        
        gameManager.DayPassed += UpdateNews;
    }

    void Start()
    {
        UpdateNews();
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
        List<int> randomValue = HelperUtils.UniqueRandomInt(0, normalNewsDict.Count, 3);
        for (int i = 0; i < newsArray.Length; i++)
        {
            newsArray[i] = normalNewsDict[randomValue[i]];
        }
    }

    private void SetNews(int scenarioType)
    {
        List<int> randomValue = HelperUtils.UniqueRandomInt(0, normalNewsDict.Count, 2);
        for (int i = 0; i < newsArray.Length ; i++)
        {
            if (i < newsArray.Length - 1)
            newsArray[i] = normalNewsDict[randomValue[i]];
            else if (i == newsArray.Length - 1)
            {
                List<News> matchedScenarios = new List<News>();
                //logic !! find template that match the scenarioType
                foreach(News news in scenarioNewsDict)
                {
                    if(news.scenario == scenarioType)
                    {
                        matchedScenarios.Add(news);
                    }
                }
                int scenarioRIndex = Random.Range(0, matchedScenarios.Count - 1);
                newsArray[i] = matchedScenarios[scenarioRIndex];
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
