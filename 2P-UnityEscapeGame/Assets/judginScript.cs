using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judginScript : MonoBehaviour
{
    //갔다왔는지 아닌지 판별하는 script
    // Start is called before the first frame update
    public Queue q1 = new Queue();

    saveManagerScript manager;
    public bool yes = false;
    void Start()
    {
        manager = GameObject.FindWithTag("Player").GetComponent<saveManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (yes)
        {

        }   
    }
}
