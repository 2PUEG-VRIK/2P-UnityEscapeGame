using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mngWhole1_2 : MonoBehaviour
{
    //1��- ����, ť������
    private int CubeNum;//1���� �ִ� ť��� ����
    private GameObject holding;//����ִ� ť��
    public GameObject[] Cubes;
    private GameObject Cube;
    private int addingWeight = 0;//��ģ ť����� ����
    private bool isHold = false;//���� ����ֳ���~
    //int holdinhCubeIndex = -1;//���� ����ִ� ť���� �ε���

    Rigidbody rigid;
    private GameObject W; //����
    //GameObject nearObject;

    //2��
    GameObject _obj;
    GameObject scrLight;
    public GameObject input;//light �Է¹޴� 
    public Text text;
    //SpriteRenderer sr;//sprite renderer 
    int check = -1;
    Image img;
    public int monNum;

    private void Start()
    {
        holding = GameObject.Find("WeaponPoint").transform.GetChild(0).gameObject;
        W = GameObject.Find("teleA");
        rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        input = GameObject.Find("Canvas_2").transform.GetChild(1).gameObject;
        scrLight = GameObject.Find("Directional Light");
        //sr = input.GetComponent<SpriteRenderer>();
        img = input.GetComponent<Image>();

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
