using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject Slide1;
    public GameObject Slide2;
    public GameObject Slide3;
    public GameObject Slide4;
   
    void Start()
    {
        Slide1.SetActive(false);
        Slide2.SetActive(false);
        Slide3.SetActive(false);
        Slide4.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ButtonDown)
        {
            
        }
    }
}
