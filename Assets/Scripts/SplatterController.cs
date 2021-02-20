using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float hue = Random.value;
        float saturation = 0.65f + (Random.value / 4);
        float value = 0.65f + (Random.value / 4);
        spriteRenderer.color = Color.HSVToRGB(hue, saturation, value);
        transform.eulerAngles = new Vector3(0, 0, Random.value*360);
        float scale = 0.5f + Random.value;
        transform.localScale = new Vector3(scale, scale,scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
