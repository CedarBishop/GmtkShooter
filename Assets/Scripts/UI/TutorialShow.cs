using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShow : MonoBehaviour
{
    private bool tutToggle = false;
    public GameObject main, tutorial;

    // Update is called once per frame

    public void Toggle()
    {
        tutToggle = !tutToggle;
    }

    // Update is called once per frame
    void Update()
    {
        if(tutToggle)
        {
            main.SetActive(false);
            tutorial.SetActive(true);
        }
        else
        {
            main.SetActive(true);
            tutorial.SetActive(false);
        }    
    }
}
