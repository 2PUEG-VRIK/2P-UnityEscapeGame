using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class theCubes : MonoBehaviour
{
    public int value;
    Rigidbody rigid;
    SphereCollider sphere;
    Ray ray;
    RaycastHit hit;
    theCubes _obj;//마우스로 선택한 큐브
    public float rotationSpeed;
    public GameObject newMonster;//상자 건들면 나오는 애들
    private int count = 0;
    GameObject clickObject;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void Update()
    {
        this.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

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

    private void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25);
        this.transform.position= Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
