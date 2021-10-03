using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theCubes : MonoBehaviour
{
    public int value;
    Rigidbody rigid;
    SphereCollider sphere;

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphere.enabled = false;
        }
    }
}
