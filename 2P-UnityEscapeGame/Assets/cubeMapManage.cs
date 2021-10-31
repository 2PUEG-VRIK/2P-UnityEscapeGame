using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cubeMapManage : MonoBehaviour
{
    public GameObject[] Cubes;
    private bool isHold = false;//���� ����ֳ���~
    Rigidbody rigid;
    theCubes cube;
    GameObject grabCube;//�տ� ����մ� ť��
    int cubeValue;//�տ� ����ִ� ť�� ��
    Ray ray;
    RaycastHit hit;
    Renderer cubeColor;
    int cubeNum = 10;//ť�� ����
    GameObject holdPosition;
    GameObject exit;
    int check = -1;

    //bool goDown;//����ǥ ũ�� ���� ����
    //GameObject tele;
    //GameObject remark;//����ǥ


    // Start is called before the first frame update
    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        grabCube = GameObject.Find("holdingCube").transform.GetChild(0).gameObject;
        holdPosition = GameObject.Find("holdingCoin");
        //tele = GameObject.Find("final").transform.GetChild(0).gameObject;

        //remark = GameObject.Find("final").transform.GetChild(1).gameObject;
       // exit = GameObject.Find("2nd").transform.GetChild(5).gameObject; �̰� ������
        //input = GameObject.Find("Canvas_2").transform.GetChild(1).gameObject;
        //scrLight = GameObject.Find("Directional Light");
        //sr = input.GetComponent<SpriteRenderer>();
        //img = input.GetComponent<Image>();
        //coinCheck = GameObject.Find("Man").GetComponent<Man>();
        //img = input.GetComponent<Image>();
        //Door = GameObject.Find("Door_5.001");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHold && check == 1)
            StartCoroutine("goBack");

        if (cubeNum == 0)
        {
            Debug.Log("ť�� 0�� ������~����");
        }

        if (cubeNum == 2)
        {
            Debug.Log("�ڸ� ���� �ΰ��� ���Ҽ�");
        }
            //if (goDown)
            //{
            //    StartCoroutine(remarkSmaller(remark));
            //}

            //else if (!goDown)
            //{
            //    StartCoroutine(remarkBigger(remark));
            //}
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Things")
        {
            if (other.transform.name == "Color_Sphere")
            {
                cube = other.transform.gameObject.GetComponent<theCubes>();
                if (!isHold)//����������� ���¿��� �ָ� ������!
                {
                    grabCube.transform.gameObject.SetActive(true);//����ְ� �ϰ�
                    grabCube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
                    cube.gameObject.SetActive(false);//���� �� ���ְ�
                    cubeValue = cube.value;//������ ����� �־�
                    isHold = true;
                }

                else // �� ���¿��� ���ڸ� ��ġ�ص�!
                {
                    if (cubeValue == cube.value)//����ִ¾ֶ� ���� �ֶ� ���� ���ٸ�
                    {
                        cube.transform.gameObject.SetActive(false);//���� �� ���ְ�,, �� �����ϱ�ȴ� ����
                        grabCube.transform.gameObject.SetActive(false);//����ִ¾� ���ְ�
                        cubeValue = -1;
                        isHold = false;
                        cubeNum -= 2;
                    }
                    else //���� �ٸ���~~~ �ٸ� �ָ� �����!
                    {
                        cube.GetComponent<Renderer>().material.color = Color.red;
                        check = 1;
                        //�� 0.5�� �ڿ� �� ���󺹱�
                        StartCoroutine(restoreColor(cube));

                    }
                }
            }

        }
    }


    IEnumerator goBack()//1������ ���ڶ� ������ �ڷ� ƨ��°�
    {
        ///rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        this.transform.Translate(new Vector3(0, 0, -30) * Time.deltaTime);

        check = -1;
        isHold = true;

        yield return null;

    }
    IEnumerator restoreColor(theCubes cube)
    {

        yield return new WaitForSeconds(0.5f);

        switch (cube.value)
        {
            case 1: // ��ȫ
                cube.GetComponent<Renderer>().material.color = new Color(255f / 255f, 181f / 255f, 242f / 255f, 255f / 255f);
                break;
            case 2: //�����
                cube.GetComponent<Renderer>().material.color = new Color(253f / 255f, 235f / 255f, 103f / 255f, 255f / 255f);
                break;
            case 3://�ϴ�
                cube.GetComponent<Renderer>().material.color = new Color(110f / 255f, 241f / 255f, 255f / 255f, 255f / 255f);
                break;
        }
    }
}
