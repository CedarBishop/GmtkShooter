using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffBadgeManager : MonoBehaviour
{
    public Color buffColor;
    public Image buffIcon;
    
    public float duration;

    private void Start()
    {
        buffIcon.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update Badge Colour Fill Slider to fade away.
        if (buffIcon.fillAmount <= 0)
        {
            Destroy(gameObject);
        }
        buffIcon.fillAmount -= (Time.deltaTime / duration);
    }
}
