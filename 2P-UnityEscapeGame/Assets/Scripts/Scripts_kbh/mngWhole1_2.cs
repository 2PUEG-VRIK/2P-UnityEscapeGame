using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mngWhole1_2 : MonoBehaviour
{
    //1층- 저울, 큐브들관련
    public GameObject[] Cubes;
    private bool isHold = false;//상자 들고있나여~
    private bool isDown;//W 내릴준비 되었나여~
    Rigidbody rigid;
    private GameObject W; //저울 & 텔레포트
    private int check_1 = -1;//1층에서 쓰이는 체크. -1이면 뛸 준비 안된거고 1이면 뛸 준비 된거
    theCubes cube;
    private int addingTotal;
    Ray ray;
    RaycastHit hit;



    //2층
    GameObject _obj;
    GameObject scrLight;
    public GameObject input;//light 입력받는 
    public Text text;
    SpriteRenderer sr;//sprite renderer 
    int check = -1;
    Image img;
    public int monNum;
    GameObject nearObject;
    Man coinCheck;
    bool isCoinHolding;//coin 들고있나여
    GameObject Door;
    private int open = 0;//문 열어
    bool isBack = false;//뒤로 한번 튕겨야지
    private GameObject target;//마우스가 클릭한 객체
    private bool _mouseState;//마우스 상태

    private void Start()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        W = GameObject.Find("teleA");
        rigid = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        input = GameObject.Find("Canvas_2").transform.GetChild(1).gameObject;
        scrLight = GameObject.Find("Directional Light"); 
        sr = input.GetComponent<SpriteRenderer>();
        img = input.GetComponent<Image>();
        coinCheck = GameObject.Find("Man").GetComponent<Man>();
        img = input.GetComponent<Image>();
        Door = GameObject.Find("Door_5.001");
        rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        isDown = cube.isDown;
    }
    //private Vector3 velocity = -Vector3.up.normalized;


    private void Update()
    {
        if (isHold && check == 1)
            StartCoroutine("goBack");

        //if (true == Input.GetMouseButtonDown(0))//마우스 내려갔나용
        //{
           
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //    }

        //}




        //2층
        if (Input.GetKeyDown(KeyCode.Return))//엔터누르면 
        {
            //문자열이랑 light랑 비교
            if (string.Compare("light", text.text, true) == 0)//정답
            {
                Answer();
            }
            else //lightㄱㅏ 아니면~
            {
                Wrong();
                Invoke("tryAgain", 0.5f);
            }
        }

        //if (coinCheck.check == 1)//동전 들고있ㄷ고
        //{
        //    isCoinHolding = true;
        //    _obj = GameObject.Find("holdingCoin").transform.GetChild(0).gameObject;
        //    _obj.SetActive(true);//동전 눈에 보이게

        //    if (open == 1)
        //    {
        //        if (isBack)
        //        {
        //            this.transform.position = Vector3.Lerp(
        //                this.transform.position, new Vector3(724, 96, 444), Time.deltaTime * 2);

        //            Invoke("mumchwo", 1);
        //        }
        //         _obj.SetActive(false);
        //        Door.transform.rotation = Quaternion.Slerp(
        //        Door.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.time * 0.001f);
        //        Door.transform.parent.GetComponent<BoxCollider>().enabled=false;
        //    }
        //}
    }
    
    private void mumchwo()//update에서 isBack=false하면 뒤로 가기도 전에 멈춰버려서~ 안됨
    {
        isBack = false;
        _obj = GameObject.Find("2nd").transform.GetChild(3).gameObject;
        Debug.Log(_obj.name);
        _obj.SetActive(true);
    }

    private void Wrong()
    {
        img.color = Color.red;
        text.text="";
    }
    private void Answer()
    {
        Destroy(input.gameObject);

        scrLight.transform.rotation = Quaternion.Euler(90, 0, 0);//암전
        _obj.SetActive(false);//What we need 없애
        _obj = null;

        _obj = GameObject.Find("Weapons").transform.GetChild(0).gameObject;  
            _obj.SetActive(true);

        for (int j = 0; j < monNum; j++)
        {
            _obj = GameObject.Find("Monsters").transform.GetChild(j).gameObject;
            _obj.SetActive(true);
        }

        //주석주석주석이~
    }
    private void tryAgain()
    {
        img.color = new Color(168,206,255,192);
        text.text.Replace(text.text, " ");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Things")
        {
            if (addingTotal >= 4 && isDown) 
            {
                W.transform.position = new Vector3(W.transform.position.x, 2.5f, W.transform.position.z);
                    //holding.SetActive(false);
                  isHold = false;
            }

            if (other.name == "teleB")//
            {
                Destroy(other.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(-90, 0, 0);//빛 off
                _obj = GameObject.Find("Canvas_2").transform.GetChild(0).gameObject;//text임
                _obj.SetActive(true);//what we need 켜
                
                input.SetActive(true);//입력받는 창 켜

                check = 2;
            }

             if(other.name=="Door" )//동전 들고 문 앞에 가면coinCheck.check==1
            {
                //문열어
                isBack = true;//뒤로 튕길 준비 완.
                open = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "PTK_Cuboid_4")
            check = -1;
    }

    IEnumerator goBack()//1층에서 상자랑 닿으면 뒤로 튕기는거
    {
        ///rigid.AddForce(Vector3.back * 15, ForceMode.Impulse);
        this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime*30);

        check = -1;
        isHold = false;

        yield return null;

    }
}