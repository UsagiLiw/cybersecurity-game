using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private bool mFaded = false;

    private float Duration = 0.5f;

    private void OnEnable()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));
    }

    private IEnumerator DoFade(CanvasGroup canvGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }
}
