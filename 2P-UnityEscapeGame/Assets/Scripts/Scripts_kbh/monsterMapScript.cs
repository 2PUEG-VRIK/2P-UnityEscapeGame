using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class monsterMapScript : MonoBehaviour
{
    //2층
    GameObject _obj;
    GameObject holdPosition;
    GameObject scrLight;
    public GameObject input;//light 입력받는 
    public Text text;
    SpriteRenderer sr;//sprite renderer 
    int check = -1;
    Image img;
    public int monNum;
    GameObject nearObject;
    Man coinCheck;
    GameObject Door;
    private int open = 0;//문 열어
    bool isBack = false;//뒤로 한번 튕겨야지
    private GameObject target;//마우스가 클릭한 객체
    private bool _mouseState;//마우스 상태
    GameObject exit;
    private bool goApartment;

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
        goApartment = false;

    }

    private void Awake()
    {

    }
    private void Update()
    {
        //2층
        if (Input.GetKeyDown(KeyCode.Return))//엔터누르면 
        {
            //문자열이랑 light랑 비교
            if (string.Compare("home", text.text, true) == 0)//정답
            {
                Answer();
            }
            else //lightㄱㅏ 아니면~
            {
                Wrong();
                Invoke("tryAgain", 0.5f);
            }
        }

        if (coinCheck.check == 1)//동전 들고있ㄷ고
        {
            holdPosition.transform.localPosition = new Vector3(0, 0.27f, -0.25f);
            _obj = GameObject.Find("holdingCoin").transform.GetChild(0).gameObject;
            _obj.SetActive(true);//동전 눈에 보이게

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

        if (goApartment)
            StartCoroutine(goApartmentCo());
    }

    private void mumchwo()//update에서 isBack=false하면 뒤로 가기도 전에 멈춰버려서~ 안됨
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
        img.color = new Color(168, 206, 255, 192);
        text.text.Replace(text.text, " ");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Exit")
        {
            goApartment = true;
            Debug.Log("Exit");
        }
        if (other.tag == "Things")
        {
            //2층
            if (other.name == "teleB")
            {
                Destroy(other.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(-90, 0, 0);//빛 off
                _obj = GameObject.Find("Canvas_2").transform.GetChild(0).gameObject;//text임
                _obj.SetActive(true);//what we eneed켜
                input.SetActive(true);//입력받는 창 켜

                check = 2;
            }

            if (other.name == "Door")//동전 들고 문 앞에 가면coinCheck.check==1
            {
                //문열어
                isBack = true;//뒤로 튕길 준비 완.
                open = 1;
            }

            
        }
    }

    IEnumerator goApartmentCo()
    {
            AsyncOperation async = SceneManager.LoadSceneAsync("md1_3");
            while (!async.isDone)
                yield return null;
    }
}

    //IEnumerator goBack()//1층에서 상자랑 닿으면 뒤로 튕기는거
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
//        case 1: // 분홍
//            cube.GetComponent<Renderer>().material.color = new Color(255f / 255f, 181f / 255f, 242f / 255f, 255f / 255f);
//            break;
//        case 2: //노란색
//            cube.GetComponent<Renderer>().material.color = new Color(253f / 255f, 235f / 255f, 103f / 255f, 255f / 255f);
//            break;
//        case 3://하늘
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


