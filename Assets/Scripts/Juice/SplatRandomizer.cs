using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatRandomizer : MonoBehaviour
{
    public Sprite[] splats;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = splats[Random.Range(0, splats.Length - 1)];
        int temp = (int)Random.Range(0, 2);
        GetComponent<SpriteRenderer>().flipY = (temp == 0) ? false : true;
    }
}
