using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class theCubes : MonoBehaviour
{
    public int value;
    Rigidbody rigid;
    SphereCollider sphere;
    public bool isDown=false;//w를 내릴 준비 되엇나여
    private int check = -1;//새로 생성되는 큐브는 하나여야하기땜시롱
    Ray ray;
    RaycastHit hit;
    theCubes _obj;//마우스로 선택한 큐브

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    }

    private void Update()
    {
        if (true == Input.GetMouseButtonDown(0))//마우스 내려갔나용
        {
            if (Physics.Raycast(ray, out hit))
            {
                _obj = hit.transform.GetComponent<theCubes>();//_obj는 선택한 큐브임 이제


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
        theCubes otherCube = other.gameObject.GetComponent<theCubes>();//가만히 있던 애는 otherCube
        if (_obj.value == otherCube.value)//잡고있는것과 닿은 큐브의 색 값이 같다면
        {
            other.transform.gameObject.SetActive(false);
            _obj.transform.gameObject.SetActive(false);
        }

        else // 색 값이 다르면
        {
            Debug.Log("색 값 다르당");
        }


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

//왜 커밋 안되냐 이번엔 될까영!