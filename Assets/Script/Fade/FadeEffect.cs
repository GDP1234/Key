using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum FadeState
{
    FadeIn = 0,
    FadeOut,
    FadeInOut,
    FadeLoopInOut,
    FadeOutIn,
    FadeLoopOutIn
}

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;


    [SerializeField]
    private AnimationCurve fadeCurve;

    private Image image;

    private FadeState fadeState; // 페이드 상태 효과

    private void Awake()
    {
        image = GetComponent<Image>();

    }


    public void OnFade(FadeState state)
    {
        fadeState = state;

        switch (fadeState)
        {
            case FadeState.FadeIn:
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut:
            case FadeState.FadeLoopInOut:
                StartCoroutine(FadeInOut());
                break;
            case FadeState.FadeOutIn:
            case FadeState.FadeLoopOutIn:
                StartCoroutine(FadeOutIn());
                break;

        }

    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0));

            yield return StartCoroutine(Fade(0, 1));

            if (fadeState == FadeState.FadeInOut)
            {
                break;
            }
        }
    }

    private IEnumerator FadeOutIn()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0, 1));

            yield return StartCoroutine(Fade(1, 0));

            if (fadeState == FadeState.FadeOutIn)
            {
                break;
            }
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            // percent가 0이라면 start를 반환 / 1이라면 end /0.5라면 start와 end사이의 중간지점을 반환
            //color.a = Mathf.Lerp(start, end, percent);

            //곡선형태
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));

            image.color = color;

            // 함수 반복 끝
            yield return null;
        }
    }
}
