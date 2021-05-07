using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameObject success;

    public Text tips_S;

    public Text result_S;

    public GameObject fail;

    public Text tips_F;

    public Text result_F;

    private void OnEnable()
    {
        success.SetActive(false);
        fail.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        GUIManager.Instance.SetActiveStatus(true, true);
        Destroy(this.gameObject);
    }

    public void SetResult(bool status, string tips, string result)
    {
        //Status: true->success, false->fail
        if (status)
        {
            success.SetActive(true);
            tips_S.text = tips;
            result_S.text = result;
        }
        else
        {
            fail.SetActive(true);
            tips_F.text = tips;
            result_F.text = result;
        }
    }

    public void ClickContinue()
    {
        this.gameObject.SetActive(false);
    }
}
