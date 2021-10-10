using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class theCubes : MonoBehaviour
{
    public int value;
    Rigidbody rigid;
    SphereCollider sphere;
    public bool isDown=false;//w�� ���� �غ� �Ǿ�����
    private int check = -1;//���� �����Ǵ� ť��� �ϳ������ϱⶫ�÷�
    Ray ray;
    RaycastHit hit;
    theCubes _obj;//���콺�� ������ ť��

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    }

    private void Update()
    {
        if (true == Input.GetMouseButtonDown(0))//���콺 ����������
        {
            if (Physics.Raycast(ray, out hit))
            {
                _obj = hit.transform.GetComponent<theCubes>();//_obj�� ������ ť���� ����


            }

        }
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
     


        if (other.gameObject.name == "teleA" )
        {
            isDown = true;
        }
    }

    
    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25);
        this.transform.position= Camera.main.ScreenToWorldPoint(mousePosition);
    }
}

//�� Ŀ�� �ȵǳ� �̹��� �ɱ!