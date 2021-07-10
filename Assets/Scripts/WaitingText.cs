using System.Collections;
using TMPro;
using UnityEngine;

public class WaitingText : MonoBehaviour
{
    [SerializeField] private int maxDots = 3;
    [SerializeField] private float delay = 0.5f;
    private TextMeshProUGUI textObject;
    private string baseText;

    public void Awake()
    {
        textObject = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        baseText = textObject.text;
    }

    public void OnDestroy()
    {
        StopWaiting();
    }


    public void SetText(string text, bool waits = false)
    {
        StopWaiting();

        baseText = text;

        if (waits)
        {
            StartWaiting();
        }
    }
    public void StartWaiting()
    {
        StopAllCoroutines();
        StartCoroutine(DotDotDot());
    }

    public void StopWaiting()
    {
        StopAllCoroutines();
    }

    public IEnumerator DotDotDot()
    {
        int dots = 0;

        while (true)
        {
            textObject.text = baseText;
            for (int i = 0; i < (dots % maxDots) + 1; i++)
            {
                textObject.text += ".";
            }

            dots++;

            yield return new WaitForSeconds(delay);
        }
    }
}
