using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monsterMapScript : MonoBehaviour
{
    //1��- ť������
    //public GameObject[] Cubes;
    //private bool isHold = false;//���� ����ֳ���~
    //Rigidbody rigid;
    //theCubes cube;
    //GameObject grabCube;//�տ� ����մ� ť��
    //int cubeValue;//�տ� ����ִ� ť�� ��
    //Ray ray;
    //RaycastHit hit;
    //Renderer cubeColor;
    //int cubeNum = 18;//ť�� ����
    //bool goDown;//����ǥ ũ�� ���� ����
    //GameObject tele;
    //GameObject remark;//����ǥ


    //2��
    GameObject _obj;
    GameObject holdPosition;
    GameObject scrLight;
    public GameObject input;//light �Է¹޴� 
    public Text text;
    SpriteRenderer sr;//sprite renderer 
    int check = -1;
    Image img;
    public int monNum;
    GameObject nearObject;
    Man coinCheck;
    GameObject Door;
    private int open = 0;//�� ����
    bool isBack = false;//�ڷ� �ѹ� ƨ�ܾ���
    private GameObject target;//���콺�� Ŭ���� ��ü
    private bool _mouseState;//���콺 ����
    GameObject exit;

    private void Start()
    {
        //ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        //input = GameObject.Find("Canvas_2").transform.GetChild(1).gameObject;
        scrLight = GameObject.Find("Directional Light");
        sr = input.GetComponent<SpriteRenderer>();
        img = input.GetComponent<Image>();
        coinCheck = GameObject.Find("Man").GetComponent<Man>();
        img = input.GetComponent<Image>();
        Door = GameObject.Find("Door_5.001");
        //rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        //grabCube = GameObject.Find("holdingCube").transform.GetChild(0).gameObject;
        // tele = GameObject.Find("final").transform.GetChild(0).gameObject;
        //remark = GameObject.Find("final").transform.GetChild(1).gameObject;
        holdPosition = GameObject.Find("holdingCoin");
        exit = GameObject.Find("2nd").transform.GetChild(5).gameObject;


    }

    private void Awake()
    {

    }
    private void Update()
    {
        //if (isHold && check == 1)
        //    StartCoroutine("goBack");

        //if (cubeNum ==0 )
        //{
        //    tele.SetActive(true);
        //    remark.SetActive(true); //����ǥ ����

        //    if (goDown)
        //    {
        //        StartCoroutine(remarkSmaller(remark));
        //    }

        //    else if (!goDown)
        //    {
        //        StartCoroutine(remarkBigger(remark));
        //    }
        //}

        //2��
        if (Input.GetKeyDown(KeyCode.Return))//���ʹ����� 
        {
            //���ڿ��̶� light�� ��
            if (string.Compare("light", text.text, true) == 0)//����
            {
                Answer();
            }
            else //light���� �ƴϸ�~
            {
                Wrong();
                Invoke("tryAgain", 0.5f);
            }
        }

        if (coinCheck.check == 1)//���� ����֤���
        {
            holdPosition.transform.localPosition = new Vector3(0, 0.27f, -0.25f);
            _obj = GameObject.Find("holdingCoin").transform.GetChild(0).gameObject;
            _obj.SetActive(true);//���� ���� ���̰�

            if (open == 1)
            {
                if (isBack)
                {
                    this.transform.position = Vector3.Lerp(
                        this.transform.position, new Vector3(724, 96, 444), Time.deltaTime * 2);

                    Invoke("mumchwo", 1);
                }
                _obj.SetActive(false);
                exit.SetActive(true);
                Door.transform.rotation = Quaternion.Slerp(
                Door.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.time * 0.001f);
                Door.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void mumchwo()//update���� isBack=false�ϸ� �ڷ� ���⵵ ���� ���������~ �ȵ�
    {
        isBack = false;
        _obj = GameObject.Find("2nd").transform.GetChild(4).gameObject;
        _obj.SetActive(true);
    }

    private void Wrong()
    {
        img.color = Color.red;
        text.text = "";
    }
    private void Answer()
    {
        Destroy(input.gameObject);

        scrLight.transform.rotation = Quaternion.Euler(90, 0, 0);//����
        _obj.SetActive(false);//What we need ����
        _obj = null;

        _obj = GameObject.Find("Weapons").transform.GetChild(0).gameObject;
        _obj.SetActive(true);

        for (int j = 0; j < monNum; j++)
        {
            _obj = GameObject.Find("Monsters").transform.GetChild(j).gameObject;
            _obj.SetActive(true);
        }

        //�ּ��ּ��ּ���~
    }
    private void tryAgain()
    {
        img.color = new Color(168, 206, 255, 192);
        text.text.Replace(text.text, " ");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Things")
        {
            //if (other.transform.name == "Cube")
            //{
            //    cube = other.transform.gameObject.GetComponent<theCubes>();
            //    if (!isHold)//����������� ���¿��� �ָ� ������!
            //    {
            //        grabCube.transform.gameObject.SetActive(true);//����ְ� �ϰ�
            //        grabCube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
            //        cube.gameObject.SetActive(false);//���� �� ���ְ�
            //        cubeValue = cube.value;//������ ����� �־�
            //        isHold = true;
            //    }

            //    else // �� ���¿��� ���ڸ� ��ġ�ص�!
            //    {
            //        if (cubeValue == cube.value)//����ִ¾ֶ� ���� �ֶ� ���� ���ٸ�
            //        {
            //            cube.transform.gameObject.SetActive(false);//���� �� ���ְ�,, �� �����ϱ�ȴ� ����
            //            grabCube.transform.gameObject.SetActive(false);//����ִ¾� ���ְ�
            //            cubeValue = -1;
            //            isHold = false;
            //            cubeNum -= 2;
            //        }
            //        else //���� �ٸ���~~~ �ٸ� �ָ� �����!
            //        {
            //            cube.GetComponent<Renderer>().material.color = Color.red;
            //            check = 1;
            //            //�� 0.5�� �ڿ� �� ���󺹱�
            //            StartCoroutine(restoreColor(cube));

            //        }
            //    }
            //}

            //2��
            if (other.name == "teleB")
            {
                Destroy(other.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(-90, 0, 0);//�� off
                _obj = GameObject.Find("Canvas_2").transform.GetChild(0).gameObject;//text��
                _obj.SetActive(true);//what we need ��

                input.SetActive(true);//�Է¹޴� â ��

                check = 2;
            }

            if (other.name == "Door")//���� ��� �� �տ� ����coinCheck.check==1
            {
                //������
                isBack = true;//�ڷ� ƨ�� �غ� ��.
                open = 1;
            }
        }
    }

    //IEnumerator goBack()//1������ ���ڶ� ������ �ڷ� ƨ��°�
    //{
    //    ///rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
    //    this.transform.Translate(new Vector3(0, 0, -30) * Time.deltaTime);

    //    check = -1;
    //    isHold = true;

    //    yield return null;

    //}
    //IEnumerator restoreColor(theCubes cube)
    //{

    //    yield return new WaitForSeconds(0.5f);

    //    switch (cube.value)
    //    {
    //        case 1: // ��ȫ
    //            cube.GetComponent<Renderer>().material.color = new Color(255f / 255f, 181f / 255f, 242f / 255f, 255f / 255f);
    //            break;
    //        case 2: //�����
    //            cube.GetComponent<Renderer>().material.color = new Color(253f / 255f, 235f / 255f, 103f / 255f, 255f / 255f);
    //            break;
    //        case 3://�ϴ�
    //            cube.GetComponent<Renderer>().material.color = new Color(110f / 255f, 241f / 255f, 255f/ 255f, 255f / 255f);
    //            break;  
    //    }
    //}

    //IEnumerator remarkBigger(GameObject r)
    //{
    //    r.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
    //    if (r.transform.localScale.x >= 30)
    //        goDown = true;
    //    yield return null;
    //}

    //IEnumerator remarkSmaller(GameObject r)
    //{
    //    r.transform.localScale -= new Vector3(0.4f, 0.4f, 0.4f);
    //    if (r.transform.localScale.x <= 20)
    //        goDown = false;
    //    yield return null;
    //}
}