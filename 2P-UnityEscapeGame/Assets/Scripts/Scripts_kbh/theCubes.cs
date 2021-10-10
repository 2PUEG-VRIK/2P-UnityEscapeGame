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
    private int check = -1;//???? ??????? ???? ???????????÷?

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
        if (other.gameObject.name == "Cube")
        {
            theCubes otherCube = other.gameObject.GetComponent<theCubes>();
            if (this.value > otherCube.value)//잡고있는게 더 클때
            {

                other.transform.localScale *= 1.1f;


                other.transform.gameObject.SetActive(false);
            }

            else
            {
                //if (check == -1)//처음 생성되는 객체!

                //{
                //    Instantiate(obj, new Vector3(
                //            (this.transform.position.x + other.transform.position.x) / 2,
                //            (this.transform.position.y + other.transform.position.y) / 2,
                //            (this.transform.position.z + other.transform.position.z) / 2),
                //            Quaternion.identity);
                //    check++;
                //}

                other.transform.localScale *= 1.2f;
                // otherCube.transform.localScale *= 1.2f;
                //this.transform.position = new Vector3(999, 999, 999);
                other.transform.gameObject.SetActive(false);

            }
        }

        if (other.gameObject.name == "teleA")
        {
            
        }

    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25);
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}

//?? Ŀ?? ???