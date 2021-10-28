using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//0 나 혼자
//1 양
//2 두더지
public class gameManager3 : MonoBehaviour
{
    public talkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction = false;
    public int talkIndex;
    private int yourIndex;//npc대화 인덱스
    private int myIndex;//내 대화 인덱스
    public int value;//npc에 따라 나가는 내 말 달라짐 (npc의 id와 동일하게 하자)
    private int myLastIndex=-1;
    private int yourLastIndex;
    private bool panelActive = false;
    private bool isMyTurn=true;//내가 대화할 차례냐~
    private bool first ;//처음 내가 말 할때만 쓰이는 변수
    private bool firstTouch = false;//npc이랑 콜라이더 처음 닿을때 쓰이는 변수. 내가 먼저 말해야해 ㅎ
    Dictionary<int, string[]> textGroup;//내 대화 뭉텅이
    private bool isCarRotate;
    private bool isCarRotateBack;

    GameObject car;
    GameObject mole;
    private bool molePopUp;
    private bool active_moleFunc;//update함수에서 코루틴 돌리게
    GameObject remark_mole;//애들 머리 위에 느낌표(예상치 못한 중요한 단서)
    GameObject arrow_blackCar;
    new Vector3 preCar;//차 원래 좌표
    new Vector3 preThing;//물건 원래 좌표

    private float time;
    private bool isTimerOn;

    private void Start()
    {
        yourIndex = 0; myIndex = 0;
        value = 0; myLastIndex = -1;
        first = true;//내가 먼저 말 시작하면서 게임 시작해야하니까
        firstTouch = false;//아직 동물이랑 안 닿은 상태니까
        isCarRotate = false; isCarRotateBack = false;
        textGroup = new Dictionary<int, string[]>();
        molePopUp = false;
        active_moleFunc = false;
        arrow_blackCar = GameObject.Find("npcArrow").transform.GetChild(1).gameObject;
        remark_mole = GameObject.Find("npcArrow").transform.GetChild(2).gameObject;
        mole = GameObject.Find("mole");
        isTimerOn = false;
        time = 0f;

        generatePlayerText();
        checkLength();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isMyTurn)
            if (first) popMyText(value);

        if (isCarRotate)
            StartCoroutine(carRotateFunc(car));
        if (!isCarRotate)
            StopCoroutine(carRotateFunc(car));
        if (active_moleFunc)
            StartCoroutine(molePopUpFunc(mole));

        if (!active_moleFunc)
            StopCoroutine(molePopUpFunc(mole));

        if(isCarRotateBack)//차 다시 원상복귀
            StartCoroutine(carRotateBackFunc(car));

        if (isTimerOn)
            time += Time.deltaTime;
        //}

    }

    private void OnTriggerEnter(Collider other) //못움직이게 해야혀
    {
        if (other.tag == "Things" )
        {
            if (other.name == "car_pivot")
            {
                preCar = other.transform.position;
                return;
            }
            else if (other.name == "Car")
            {

            }
            else {
                preThing = other.transform.position;
                Debug.Log("두더지 콜라 안에 들어와서 my turn true됨");
                isMyTurn = true;
                checkLength();
            }

            talkPanel.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Things")
        {
            if (Input.GetKeyDown(KeyCode.X))//대화가능 npc
            {
                Action(other.transform.gameObject);
            }

            else if (Input.GetKeyDown(KeyCode.LeftAlt))//단서 주는 물건들
            {
                if (other.gameObject.name == "car_pivot")
                {
                    car = other.gameObject;
                    isCarRotate = true;
                    //  if (Input.GetKeyDown(KeyCode.LeftShift))
                    //StartCoroutine(carRotate(other.gameObject));
                }

                if (other.gameObject.name == "mole")//두더지 파묻혀있는거 처음 발견하고 꺼내는 과정
                    if (molePopUp)
                        active_moleFunc = true;//mole popup 코루틴 돌릴준비 완료
            }
        }
    }

    public void Action(GameObject scanObj)
    {
        //  playerText();
        if (isAction)
            isAction = false;

        else //Action==false
        {
            scanObject = scanObj;
            objectData objData = scanObject.GetComponent<objectData>();
            isAction = true;

            Talk(objData.id, objData.isNpc);
            talkPanel.SetActive(true);
            panelActive = true;
            value = objData.id;
            myLastIndex = textGroup[objData.id].Length;//내 대화 길이 체크하고
            firstTouch = false;

            if (myIndex <= myLastIndex)//내 대화가 끝나기 전까지만 애랑 대화 주고받기
            {
                if (isMyTurn)
                    popMyText(value);
                else
                    popNPCText(value);
            }
        }
    }
   

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Things")
        {
            if (other.gameObject.name == "car_pivot")
            { 
                isCarRotate = false;
                arrow_blackCar.SetActive(true);
            }

            if (other.gameObject.name == "mole")
            {
                isCarRotateBack = true;//나갔으니까 두더지 내려가고 차 위치나 회전 원상복귀
            }
            yourIndex = 0; myIndex = 0;
            value = 0;
            talkText.text = "";
        }
    }   

    void Talk(int id, bool isNpc)
    {
        talkManager.GetTalk(id, talkIndex);
    }

    private void generatePlayerText()//내 대화 제작
    {
        //게임 시작 후 바로 나오는 대화
        textGroup.Add(0, new string[] { "(헉.. 헉..) 누렁아! 누렁아!!", "어디있는거야..." });//mylast=2
        //양이랑 하는 대화
        textGroup.Add(1, new string[]//mylast=3
        { "안녕?", "나는 두두야. 혹시 여기\n노란 강아지 지나가는거 못봤니?","그렇구나.. 실례했어! 안녕!"});
        //두더지랑 대화
        textGroup.Add(2, new string[]
        {
            "나(1)","나(3)","나(끝)"
        });

        textGroup.Add(3, new string[]
        {
            "나(1)", "나(3)", "나(5)"
        });
    }

    private string GetMyTalk(int id, int myIndex)
    {
        return textGroup[id][myIndex];
    }

    private void checkLength()//내 대화 길이 체크
    {
        myLastIndex = textGroup[value].Length;

        talkPanel.SetActive(true);
        panelActive = true;
    }

    private void popMyText(int value) { // npc랑 하는 내 대화 띄우기

        if (value == 0)
        { //혼자 뛰어다니는 상황 설명
          // if (myLastIndex <= myIndex) //대화의 끝에 도달하면 
            while (true)
            {
                if (myLastIndex <= myIndex)
                {
                    talkPanel.SetActive(false);
                    panelActive = false;
                    yourIndex = 0; myIndex = 0;
                    first = false;
                    Debug.Log("대화 끝났다는");
                    break;
                }
                if (Input.GetKeyDown(KeyCode.X))

                {
                    talkText.text = GetMyTalk(value, myIndex);
                    myIndex++;
                    Debug.Log(myIndex);
                    break;

                }
            }
        }

        else
        { //npc랑 대화할 때 내 말들
          // if (myLastIndex <= myIndex) //내 대화 끝에 도달
            while (true)
            {
                if (myLastIndex <= myIndex)
                {
                    talkPanel.SetActive(false);
                    panelActive = false;
                    Debug.Log("대화 끝났다는");

                    yourIndex = 0; myIndex = 0;
                    yourLastIndex = 0; myLastIndex = 0;
                    break;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    talkText.text = GetMyTalk(value, myIndex);
                    myIndex++;
                    if (Input.GetKeyDown(KeyCode.X)) Debug.Log("X 눌림");
                    Debug.Log("현재 내 인덱스 " + myIndex + "    끝 인덱스 " + myLastIndex);
                    break;

                }
            }
            isMyTurn = false;
        }
    }

    private void popNPCText(int value)
    {

        yourLastIndex = talkManager.CheckLength(value);

        //if (yourLastIndex <= yourIndex)
        while (true)
        {
            if (yourLastIndex <= yourIndex)
            {
                talkPanel.SetActive(false);
                panelActive = false;
                Debug.Log("대화 긑나서 창 종료");
                yourIndex = 0; myIndex = 0;
                yourLastIndex = 0; myLastIndex = 0;
                break;
            }
            if (Input.GetKeyDown(KeyCode.X)) 
            {
                Debug.Log("X 눌림");
                talkText.text = talkManager.GetTalk(value, yourIndex);//npc index 대화 출력
                yourIndex++;
                isMyTurn = true;
                Debug.Log("현재 두더지 인덱스 " + yourIndex + "끝 인덱스 " + yourLastIndex);
                break;

            }
        }
    }

    IEnumerator carRotateFunc(GameObject car)
    {
        car.transform.position = new Vector3(car.transform.position.x, car.transform.position.y + 2f, car.transform.position.z);

        arrow_blackCar.SetActive(false);
        //236.7, -9,-91.2
        car.transform.rotation = Quaternion.Slerp(
               car.transform.localRotation, Quaternion.Euler(new Vector3(0, 180, 80f)), Time.time * 0.01f);
        
        yield return new WaitForSecondsRealtime(2f);
        //if (car.transform.rotation == Quaternion.Euler(new Vector3(0, 180, 50f)))
        mole.GetComponent<BoxCollider>().enabled = true;
        car.GetComponent<BoxCollider>().enabled = false;
        molePopUp = true;
        remark_mole.SetActive(true);
        isCarRotate = false;

        // StopCoroutine(carRotateFunc(car));
    }

    IEnumerator carRotateBackFunc(GameObject car)
    {
        //236.7, -9,-91.2
        mole.transform.Translate(new Vector3(0, -0.7f, 0));
        //yield return new WaitForSecondsRealtime(6f);
        if (mole.transform.localPosition.y <= preThing.y)
        {
            molePopUp = false;
            active_moleFunc = false;
        }
        car.transform.position =preCar;
        car.transform.rotation = Quaternion.Slerp(
               car.transform.localRotation, Quaternion.Euler(new Vector3(0, 180, 0)), Time.time * 0.01f);

        yield return new WaitForSecondsRealtime(0.16f);
        //if (car.transform.rotation == Quaternion.Euler(new Vector3(0, 180, 50f)))
        mole.GetComponent<BoxCollider>().enabled = false;
        car.GetComponent<BoxCollider>().enabled = true;
        molePopUp = false;
        remark_mole.SetActive(false);
        isCarRotate = false; isCarRotateBack = false;
        arrow_blackCar.SetActive(true);


        Invoke("FlowerSay", 4f);


        // StopCoroutine(carRotateFunc(car));
    }

    IEnumerator molePopUpFunc(GameObject mole)
    {
        remark_mole.SetActive(false);
        mole.transform.Translate(new Vector3(0, 1f, 0));
        //yield return new WaitForSecondsRealtime(6f);
        if (mole.transform.localPosition.y >= -11f)
        { 
            molePopUp = false;
            active_moleFunc = false;
        }
        yield return null;
    }

    private void FlowerSay()
    {
        talkPanel.SetActive(true);
        panelActive = true;
        talkText.text = "야! 너! 이리와 봐!";

        isTimerOn = true;
        if (time > 2.5f)
            talkText.text = "어? 날 부르는건가?";
        if (time > 5f)
            talkText.text = "그래 너 ~";


    }
  //mole=-11.87

}