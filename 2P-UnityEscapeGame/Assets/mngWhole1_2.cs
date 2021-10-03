using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mngWhole1_2 : MonoBehaviour
{
    //1��- ����, ť������
    private int CubeNum;//1���� �ִ� ť��� ����
    private GameObject holding;//����ִ� ť��
    public GameObject[] Cubes;
    private GameObject Cube;
    private int addingWeight = 0;//��ģ ť����� ����
    private bool isHold = false;//���� ����ֳ���~
    int holdinhCubeIndex = -1;//���� ����ִ� ť���� �ε���

    Rigidbody rigid;
    GameObject nearObject;

    private void Start()
    {
        holding = GameObject.Find("WeaponPoint").transform.GetChild(0).gameObject;

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Things")
        {
            if (other.name == "Cube")
            {
                if (!isHold)
                {//�ȵ���ִ� ä�� ť�� ������
                    theCubes cube = other.GetComponent<theCubes>();
                    addingWeight += cube.value;
                    Destroy(other.gameObject);
                    holding.SetActive(true);
                    isHold = true;
                    //ť�� ��� �ٴڿ� �ִ� ť�� �����
                    Debug.Log("�ȵ���ִµ� �������");


                }

                else //������� ��
                {
                    theCubes cube = other.GetComponent<theCubes>();
                    other.isTrigger = false;
                    cube.transform.localScale += new Vector3(0.7f,0.7f,0.75f);
                    holding.SetActive(false);
                    Vector3 reactVec = transform.position - other.transform.position;
                    reactVec = reactVec.normalized;
                    reactVec += Vector3.back;
                    
                    Debug.Log("����ִµ� �������");
                    isHold = false;


                }

            }
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Things")
        {
            if (other.name == "Cube")
            {
                other.isTrigger = true;

            }
        }
    }
}
