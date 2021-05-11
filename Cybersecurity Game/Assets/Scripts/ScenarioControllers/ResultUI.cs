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

    public GameObject endGame_Prefab;

    private bool GameOver = false;

    private void OnEnable()
    {
        success.SetActive(false);
        fail.SetActive(false);
        GUIManager.Instance.SetActiveStatus(false, false);
    }

    private void OnDisable()
    {
        GUIManager.Instance.SetActiveStatus(true, true);
        Destroy(this.gameObject);
    }

    public void SetResult(
        bool status,
        string tips,
        string result,
        bool gameOver
    )
    {
        //Status: true->success, false->fail
        if (status)
        {
            success.SetActive(true);
            tips_S.text = tips;
            result_S.text = result;
            FindObjectOfType<AudioManager>().Play("sfx_success");
        }
        else
        {
            fail.SetActive(true);
            tips_F.text = tips;
            result_F.text = result;
            FindObjectOfType<AudioManager>().Play("sfx_fail");
        }
        GameOver = gameOver;
    }

    public void ClickContinue()
    {
        Time.timeScale = 1f;
        if (GameOver == true)
            TriggerGameOver();
        else
            this.gameObject.SetActive(false);
    }

    public void TriggerGameOver()
    {
        GameObject gameOver = Instantiate(endGame_Prefab) as GameObject;
        gameOver.transform.SetParent(GameObject.Find("GUI").transform);
        gameOver.transform.SetAsLastSibling();
    }
}
