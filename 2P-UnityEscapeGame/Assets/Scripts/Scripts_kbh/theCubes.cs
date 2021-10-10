using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class theCubes : MonoBehaviour
{
    public int value;
    public GameObject obj;//???? ????????? ???
    Rigidbody rigid;
    SphereCollider sphere;
    private int check = -1;//???? ??????? ???? ???????????��?

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphere.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.name == "Cube")
        //{
        //    theCubes otherCube = other.gameObject.GetComponent<theCubes>();
        //    if (this.value > otherCube.value)//잡고있는게 더 클때
        //    {
               
        //        other.transform.localScale *= 1.1f;

        //        other.transform.gameObject.SetActive(false);
        //    }

        //    else
        //    {
               
        //        other.transform.localScale *= 1.2f;
        //        other.transform.gameObject.SetActive(false);

        //    }
        //}

        //if (other.gameObject.name == "teleA")
        //{
        //}

    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25);
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
