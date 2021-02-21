using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {        
        
    }

    public void spawnSprite(float speed)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float hue = Random.value;
        float saturation = 0.65f + (Random.value / 4);
        float value = 0.65f + (Random.value / 4);
        spriteRenderer.color = Color.HSVToRGB(hue, saturation, value);
        transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        float scale = (0.775f + (Random.value)/4)* 0.5f + (speed/15);
        transform.localScale = new Vector3(scale, scale, scale);
        int index = (int)(Random.value * sprites.Length);
        if (index == sprites.Length) index = sprites.Length-1;
        spriteRenderer.sprite = sprites[(int)(Random.value * sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
