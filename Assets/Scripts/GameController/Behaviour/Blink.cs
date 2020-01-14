using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public Image image;
    public void startBlink()
    {
        StartCoroutine(blink(3.0f));
    }


    IEnumerator blink(float waitTime)
    {
        var endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            image.enabled = true;
            yield return new WaitForSeconds(0.5f);
            image.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
