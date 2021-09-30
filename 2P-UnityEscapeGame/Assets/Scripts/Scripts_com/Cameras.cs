using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public Camera main;
    public Camera man;
    public Camera waman;


    // Start is called before the first frame update
    void Start()
    {
        mainClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mainClick()
    {
        main.enabled = true;
        man.enabled = false;
        waman.enabled = false;
    }
    public void manClick()
    {
        main.enabled = false;
        man.enabled = true;
        waman.enabled = false;
    }
    public void wamanClick()
    {
        main.enabled = false;
        man.enabled = false;
        waman.enabled = true;
    }
}
