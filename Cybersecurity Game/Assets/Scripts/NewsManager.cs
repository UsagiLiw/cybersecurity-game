using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    private static EmailObject[] newsDict;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        gameManager.DayPassed += UpdateNews;
    }

    public void SetDictionaries()
    {
        SetNewsDictionary();
    }

    private void SetNewsDictionary()
    {
        string jsonString = SaveSystem.LoadDictionary("NewsTemplate.json");
        if (jsonString == null)
        {
            Debug.Log("Error - Unable to find EmailTemplate.json");
            Application.Quit();
        }
        newsDict = JsonHelper.FromJson<EmailObject>(jsonString);
    }

    private void UpdateNews()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class News
{
    public string template;
    public int scenario;
    public string topic;
    public string content;

}
