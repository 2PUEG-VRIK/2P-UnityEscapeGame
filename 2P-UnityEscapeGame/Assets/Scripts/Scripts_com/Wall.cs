using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    //private int monsterNum = GameObject.Find("Man").GetComponent<Permanent>().number;



   

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            transform.position += Vector3.up * -10;
        }


    }
}
