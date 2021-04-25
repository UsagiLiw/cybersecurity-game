using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanUnsecureController : MonoBehaviour
{

    public GameObject box;
    public Text threatDetail;
    public Antivirus antivirus;

    [SerializeField] private float cleanTime;
    [SerializeField] private Image cleanBar;

    private List<Text> instantiatedThreats = new List<Text>();
    private float timer;

    private void OnEnable()
    {
        UpdateSummary();
    }

    private void OnDisable()
    {
        ClearSummary();
    }

    public void CleanButtonPressed()
    {
        Debug.Log("Clean button pressed");
        StartCoroutine(CleanAnimation());
    }

    IEnumerator CleanAnimation()
    {
        timer = 0;
        while (timer < cleanTime)
        {
            timer += Time.deltaTime;
            cleanBar.fillAmount = timer / cleanTime;
            yield return null;
        }
        ComputerManager.activeComputer.malware.Clear();
        Debug.Log("Clean finished");
        ClearSummary();
        antivirus.OpenSecurePage();
        ComIsClean();
    }

    private void UpdateSummary()
    {
        ClearSummary();
        List<int> malwareList = ComputerManager.activeComputer.malware;
        foreach (int malware in malwareList)
        {
            Text newThreat = Instantiate(threatDetail) as Text;
            newThreat.text = MalwareManager.malwareDict[malware - 1].name + " - " + MalwareManager.malwareDict[malware - 1].detail;
            newThreat.transform.SetParent(box.transform, false);
            instantiatedThreats.Add(newThreat);
            Debug.Log("Threat Detail show" + malware);
        }
    }

    private void ClearSummary() 
    {
        foreach (Text threat in instantiatedThreats)
        {
            Destroy(threat.gameObject);
        }
        instantiatedThreats.Clear();
    }

    private void ComIsClean()
    {

    }
}
