using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private float Duration = 2.5f;

    private void OnEnable()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        StartCoroutine(DoFade(canvGroup));
    }

    private IEnumerator DoFade(CanvasGroup canvGroup)
    {
        float counter = 0f;
        canvGroup.alpha = 0;
        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(0, 1, counter / Duration);

            yield return null;
        }
    }

    public void BackToMainMenu()
    {
        Debug.Log("back to main menu");
        GameManager.BackToMainMenu(false);
        Destroy(this.gameObject);
    }
}
