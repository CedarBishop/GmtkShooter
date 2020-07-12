using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bob : MonoBehaviour
{
    private Vector3 startPos;

    public float height = 10;
    public float speed = 1;
    public float shift = 0;
    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        rect.anchoredPosition = startPos + new Vector3(0f, height * Mathf.Sin(Time.unscaledTime * speed) + shift, 0f);
    }
}
