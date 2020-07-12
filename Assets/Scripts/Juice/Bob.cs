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

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector3(0f, height * Mathf.Sin(Time.unscaledTime * speed) + shift, 0f);  
    }
}
