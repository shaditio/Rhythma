using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int noteCount;
    public float score;
    public int perfectHit = 0;
    public int greatHit = 0;
    public int goodHit = 0;
    public int badHit = 0;
    public GameObject accuracyText;
    private Color perfectColor = new Color(251, 255, 0, 255);
    private Color greatColor = new Color(242, 0, 255, 255);
    private Color goodColor = new Color(0, 255, 0, 255);
    private Color badColor = new Color(0, 179, 255, 255);
    private Color missColor = new Color(130, 130, 130, 255);
    private const float MAXTOTALSCORE = 1000000;
    private float fadeOutTime = 0.5f;
    private Coroutine fadeCoroutine = null;

    public void scoreJudgement(Vector3 notePosition, Vector3 hitboxPosition)
    {
        float maxScore = MAXTOTALSCORE / noteCount;
        if (Mathf.Abs(notePosition.z - hitboxPosition.z) <= 0.2)
        {
            fadeOutAccuracy("Perfect");
            perfectHit++;
            score += maxScore;
        }
        else if (Mathf.Abs(notePosition.z - hitboxPosition.z) <= 0.4)
        {
            fadeOutAccuracy("Great");
            greatHit++;
            score += maxScore * 0.8f;
        }
        else if (Mathf.Abs(notePosition.z - hitboxPosition.z) <= 0.6)
        {
            fadeOutAccuracy("Good");
            goodHit++;
            score += maxScore * 0.5f;
        }
        else
        {
            fadeOutAccuracy("Bad");
            badHit++;
            score += maxScore * 0.3f;
        }
    }

    public void notifyMiss()
    {
        fadeOutAccuracy("Miss");
    }

    public int getMissHit()
    {
        return noteCount - perfectHit - greatHit - goodHit - badHit;
    }

    public void fadeOutAccuracy(string variant)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(fadeOutRoutine(variant));
    }
    private IEnumerator fadeOutRoutine(string variant)
    {
        Text text = accuracyText.GetComponent<Text>();
        text.text = variant;

        Color originalColor = text.color;

        switch (variant)
        {
            case "Perfect":
                originalColor = perfectColor;
                break;
            case "Great":
                originalColor = greatColor;
                break;
            case "Good":
                originalColor = goodColor;
                break;
            case "Bad":
                originalColor = badColor;
                break;
            default:
                originalColor = missColor;
                break;
        }

        for (float t = 0.01f; ; t += Time.deltaTime)
        {
            if (t >= fadeOutTime)
            {
                text.color = Color.clear;
                break;
            }
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));
            yield return null;
        }
    }
}
