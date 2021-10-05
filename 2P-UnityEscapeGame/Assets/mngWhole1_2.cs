using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mngWhole1_2 : MonoBehaviour
{
    //1층- 저울, 큐브들관련
    private int CubeNum;//1층에 있는 큐브들 개수
    private GameObject holding;//들고있는 큐브
    public GameObject[] Cubes;
    private GameObject Cube;
    private int addingWeight = 0;//합친 큐브들의 무게
    private bool isHold = false;//상자 들고있나여~
    //int holdinhCubeIndex = -1;//지금 들고있는 큐브의 인덱스

    Rigidbody rigid;
    private GameObject W; //저울
    //GameObject nearObject;

    //2층
    GameObject _obj;
    GameObject scrLight;
    public GameObject input;//light 입력받는 
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
                {//안들고있는 채로 큐브 만나면
                    theCubes cube = other.GetComponent<theCubes>();
                    addingWeight += cube.value;
                    Destroy(other.gameObject);
                    holding.SetActive(true);
                    isHold = true;
                    //큐브 들고 바닥에 있던 큐브 사라짐
                    Debug.Log("안들고있는데 만났어요");


                }

                else //들고있을 때
                {
                    theCubes cube = other.GetComponent<theCubes>();
                    other.isTrigger = false;
                    cube.transform.localScale += new Vector3(0.7f,0.7f,0.75f);
                    holding.SetActive(false);
                    Vector3 reactVec = transform.position - other.transform.position;
                    reactVec = reactVec.normalized;
                    reactVec += Vector3.back;
                    
                    Debug.Log("들고있는데 만났어요");
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
