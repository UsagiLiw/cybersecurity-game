using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrojanBehavior : MonoBehaviour
{
    public TrojanIconClass[] iconList;

    void Start()
    {
        int index = Random.Range(0, iconList.Length - 1);
        Image image = gameObject.transform.GetChild(0).gameObject.transform.GetComponent<Image>();
        Text name = gameObject.transform.GetChild(1).gameObject.transform.GetComponent<Text>();
        
        name.text = iconList[index].name;
        image.sprite = iconList[index].image;
        ComputerManager.NewActiveComAction += IsAlive;
    }

    void OnDisable()
    {
        ComputerManager.NewActiveComAction -= IsAlive;
        Destroy(this.gameObject);
    }

    public void InstantFailure()
    {
        Debug.Log("You failed");
        MalwareController malwareController = GameObject.Find("ScenarioManager").GetComponent<MalwareController>();
        malwareController.InvokeScenarioFailure();
    }

    private void IsAlive()
    {
        if(!ComputerManager.activeComputer.isInfected)
            this.gameObject.SetActive(false);
    }
}

[System.Serializable]
public class TrojanIconClass
{
    public string name;

    public Sprite image;
}
