using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mngWhole1_2 : MonoBehaviour
{
    //1층- 저울, 큐브들관련
    private GameObject holding;//들고있는 큐브
    public GameObject[] Cubes;
    private int addingWeight = 25;//합친 큐브들의 무게
    private bool isHold = false;//상자 들고있나여~
    int holdinhCubeIndex = -1;//지금 들고있는 큐브의 인덱스

    Rigidbody rigid;
    private GameObject W; //저울
    GameObject nearObject;

    //2층
    GameObject _obj;
    GameObject scrLight;
    public GameObject input;//light 입력받는 
    public Text text;
    SpriteRenderer sr;//sprite renderer 
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
        sr = input.GetComponent<SpriteRenderer>();
        img = input.GetComponent<Image>();

    }
    // Update is called once per frame
    private Vector3 velocity = -Vector3.up.normalized;

    private void Update()
    {
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

            

        
    }
    private void tryAgain()
    {
        img.color = new Color(168,206,255,192);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Things")
        {
            if (other.name == "Cube")
            {
                if (!isHold)
                {//안들고있는 채로 큐브 만나면
                    theCubes cube = other.GetComponent<theCubes>();
                    addingWeight += cube.value;
                    holding.SetActive(true);
                    isHold = true;
                    other.gameObject.SetActive(false);
                    //큐브 들고 바닥에 있던 큐브 사라짐


                }

                else //들고있을 때
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
                Debug.Log("닿았다");
                Destroy(other.gameObject);

                scrLight.transform.rotation = Quaternion.Euler(-90, 0, 0);//빛 off
                _obj = GameObject.Find("Canvas_2").transform.GetChild(0).gameObject;//text임
                _obj.SetActive(true);//what we need 켜

                input.SetActive(true);//입력받는 창 켜
               
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