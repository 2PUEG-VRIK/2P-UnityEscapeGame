using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mngWhole1_2 : MonoBehaviour
{
    //1��- ����, ť������
    private GameObject holding;//����ִ� ť��
    public GameObject[] Cubes;
    private int addingWeight = 25;//��ģ ť����� ����
    private bool isHold = false;//���� ����ֳ���~
    int holdinhCubeIndex = -1;//���� ����ִ� ť���� �ε���

    Rigidbody rigid;
    private GameObject W; //����
    GameObject nearObject;

    //2��
    GameObject _obj;
    GameObject scrLight;
    public InputField input;//light �Է¹޴� 
    public Text text;
    SpriteRenderer sr;//sprite renderer 
    int check = -1;
    Image img;

    private void Start()
    {
        holding = GameObject.Find("WeaponPoint").transform.GetChild(0).gameObject;
        W = GameObject.Find("teleA");
        rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        scrLight = GameObject.Find("Directional Light");
        sr = input.GetComponent<SpriteRenderer>();
        img = input.GetComponent<Image>();

    }
    // Update is called once per frame
    private Vector3 velocity = -Vector3.up.normalized;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))//���ʹ����� 
        {
            Debug.Log(input.text);
            //���ڿ��̶� light�� ��
            if (string.Compare("light", text.ToString(), true) == 0)//����
            {
                Destroy(input.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(90, 0, 0);//����
                _obj.SetActive(false);//What we need ����

            }
            else //light���� �ƴϸ�~
            {
                //sr.color = Color.red;
                img.color = Color.red;
                text = null;
            }
        }

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
                    holding.SetActive(true);
                    isHold = true;
                    other.gameObject.SetActive(false);
                    //ť�� ��� �ٴڿ� �ִ� ť�� �����


                }

                else //������� ��
                {
                    theCubes cube = other.GetComponent<theCubes>();
                    other.isTrigger = true;
                    holding.SetActive(false);

                    Vector3 reactVec = transform.forward * Random.Range(-10, -20) + Vector3.up * Random.Range(10, 20);
                    rigid.AddForce(reactVec, ForceMode.Impulse);
                    cube.gameObject.SetActive(true);
                    cube.transform.localScale += new Vector3(1f, 1f, 1f);

                    //Vector3 target = -Vector3.forward.normalized*7f;
                    //transform.position = Vector3.Lerp(transform.position, transform.position+target, 0.1f);
                    isHold = false;

                }

            }

            if (other.name == "PTK_Cuboid_4" && addingWeight == 25)
            {
                W.transform.position = new Vector3(W.transform.position.x, 2.5f, W.transform.position.z);

                if (transform.position.y > 3)//
                {
                    holding.SetActive(false);
                    isHold = false;
                }
            }

            if (other.name == "teleB")//
            {
                Debug.Log("��Ҵ�");
                Destroy(other.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(-90, 0, 0);//�� off
                _obj = GameObject.Find("Canvas_2").transform.GetChild(0).gameObject;//text��
                _obj.SetActive(true);//what we need ��

                input.enabled=true;//�Է¹޴� â ��

                check = 2;
            }

        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PTK_Cuboid_4")
            check = -1;
    }
}