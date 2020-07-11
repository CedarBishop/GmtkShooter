using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopColour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        float random = Random.Range(0f, 255f);
        float saturation = 0.7f;
        if (random >= 80 && random <= 180)
        {
            saturation -= 0.2f;
        }
        spriteRenderer.color = Color.HSVToRGB(random/255f, saturation, 1);
    }
}
