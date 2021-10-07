using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class theCubes : MonoBehaviour
{
    public int value;
    Rigidbody rigid;
    SphereCollider sphere;

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

    public int addNum = 0;//ť�� ��ģ ����.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cube")
        {
            theCubes otherCube = other.gameObject.GetComponent<theCubes>();

            if (this.value > otherCube.value)//����ִ°� �� Ŭ��
            {
                otherCube.transform.localScale = this.transform.localScale * 1.1f;
                this.transform.position = new Vector3(999, 999, 999);
            }

            else
            {
                otherCube.transform.localScale *= 1.2f;
                this.transform.position = new Vector3(999, 999, 999);
            }
            addNum++;
            Debug.Log(addNum);
        }
    }
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25);
        this.transform.position= Camera.main.ScreenToWorldPoint(mousePosition);
    }
}

//�� Ŀ�� �ȵǳ�