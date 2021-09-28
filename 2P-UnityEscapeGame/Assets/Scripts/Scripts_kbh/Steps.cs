using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    public GameObject broken1;
    public GameObject broken2;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Break", 1.5f);

        }
      
    }

    void Break()
    {
        this.gameObject.SetActive(false);
        broken1.SetActive(true);
        broken2.SetActive(true);

    }
}
