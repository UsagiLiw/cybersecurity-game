using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private float Duration = 2.5f;

    private void OnEnable()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        canvGroup.alpha = 0;
        StartCoroutine(DoFade(canvGroup, 0f, 1f));
    }

    private IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;
        GUIManager.Instance.SetActiveStatus(false, false);
        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
        Time.timeScale = 0f;
    }

    public void BackToMainMenu()
    {
        GameManager.BackToMainMenu(false);
        Destroy(this.gameObject);
        Time.timeScale = 1f;
    }
}
