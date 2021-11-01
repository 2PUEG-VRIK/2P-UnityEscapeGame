using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// npc 만나는 순서
/// 양 -> 대화끝나면 꽃이 부름 -> 꽃한테 감 -> 속아서 아파트 감 -> monsterMap ->꽃한테 따지러 감
/// -> 제대로 알려줘 그 곳으로 감 -> 문이 고장나 있음 ->
/// </summary>
//value
//0 나 혼자
//1 양
//2 두더지
//3 꽃

/*check(초기값 0)
1- 차 회전 원상복구 끝나고 꽃이 말 걸 때 필요해서
-1- 꽃이 말 거는거 끝나면 

2 (꽃 말 듣고) 아파트로 가서 specialPlane 밟아->monsterMap으로 이동까지.
-2 houseTalk코루틴 중단시키는 조건. monsterMap에서 여기로 돌아오면 check=-2

3 문앞에 있는 쪽지 발견! 오리 찾으러 가

*/
public class gameManager3 : MonoBehaviour
{
    public talkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    private GameObject scanObject;

    public GameObject namePanel;//누가 말하는지 뜨는 패널
    public Image nameIcon;//말하는 애 아이콘 뜨는 곳~
    public Text nameText;//이름 뜨는 text

    //public bool isAction = false;
    public int talkIndex;
    public int yourIndex;//npc대화 인덱스
    public int myIndex;//내 대화 인덱스
    public int value;//npc에 따라 나가는 내 말 달라짐 (npc의 id와 동일하게 하자)
    private int myLastIndex = -1;
    private int yourLastIndex;
    private bool panelActive = false;
    private bool isMyTurn = true;//내가 대화할 차례냐~
    private bool first;//처음 내가 말 할때만 쓰이는 변수
    private bool firstTouch = false;//npc이랑 콜라이더 처음 닿을때 쓰이는 변수. 내가 먼저 말해야해 ㅎ
    private bool isCarRotate;
    private bool isCarRotateBack;
    Dictionary<int, string[]> textGroup;//내 대화 뭉텅이
    Dictionary<int, string[]> nameTextGroup;//말하는 사람들 이름 뭉텅이


    GameObject car;
    GameObject mole;
    private bool molePopUp;
    private bool active_moleFunc;//update함수에서 코루틴 돌리게
    GameObject remark_mole;//애들 머리 위에 느낌표(예상치 못한 중요한 단서)
    GameObject arrow_blackCar;
    Vector3 preCar;//차 원래 좌표
    Vector3 preThing;//물건 원래 좌표

    private float time;//꽃 관련 시간 on
    private bool isTimerOn;
    public int check;//여러곳에 쓰일 변수

    public Sprite[] images;
    private GameObject touchThings;//닿은 물체
    private bool isTouch;//콜라이더 닿았을 때 true놓는 변수

    //맵 간 이동
    AudioListener audioListener;//이동할 때 이전 맵의 오디오 리스너 끄기
    saveManagerScript data;
    //public static List<int> l1 = new List<int>();//몬스텁 가기 전 맵의 모든 정보
    //static List<int> l2= new List<int>();
    public bool alreadyCame = false;//맵 한번 갔다온거임~
    //GameObject saveM;
    GameObject judge;//씬 이동 했는지 판별
    judginScript judgeSc;
    private bool twice = false;//한 번 이동하면 그 후로 쭉 true


    //꽃이 알려준대로 문에 감
    GameObject exitDoor;
    GameObject hidden;//door 앞 hiddenPlane

    private void Start()
    {
        yourIndex = 0; myIndex = 0;
        value = 0; myLastIndex = -1;
        first = true;//내가 먼저 말 시작하면서 게임 시작해야하니까
        firstTouch = false;//아직 동물이랑 안 닿은 상태니까
        isCarRotate = false; isCarRotateBack = false;
        textGroup = new Dictionary<int, string[]>();
        nameTextGroup = new Dictionary<int, string[]>();
        molePopUp = false;
        active_moleFunc = false;
        arrow_blackCar = GameObject.Find("npcArrow").transform.GetChild(1).gameObject;
        remark_mole = GameObject.Find("npcArrow").transform.GetChild(2).gameObject;
        mole = GameObject.Find("mole");
        audioListener = GameObject.Find("PlayerCam").GetComponent<AudioListener>();
        judge = GameObject.Find("judging");
        judgeSc = judge.GetComponent<judginScript>();
        //saveM = GameObject.Find("saveManager");
        //data = saveM.GetComponent<saveManagerScript>();
        isTimerOn = false;
        isTouch = false;
        alreadyCame = false;
        check = 0;
        time = 0f;
        //GameObject plane = GameObject.Find("specialPlane");
        //if (judge.GetComponent<judginScript>().yes)
        //    plane.SetActive(false);
        
        generatePlayerText();
        generateNameText();
        checkLength();

        if (GameObject.Find("judging").GetComponent<judginScript>().yes)//갔다옴
        {
            Debug.Log("judging");
            GameObject.Find("specialPlane").SetActive(false);
            nameText.text = GetName(0, 0);
            changeNameIcon(0);
            talkText.text = "가서 따져야겠어";
            first = false;
            myIndex = (int)judgeSc.arr1[5];
            yourIndex = (int)judgeSc.arr1[6];
            Debug.Log("myindex" + myIndex);
            Debug.Log("yours" + yourIndex);
            twice = true;
        }

        exitDoor = GameObject.Find("exitDoor");
        hidden = GameObject.Find("hiddenPlane_1");

    }

    private void Awake()
    {
        //var obj = GameObject.FindGameObjectsWithTag("dont") ;
        //if (obj.Length != 1)
        //    Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isMyTurn)
            if (first) popMyText(value);

        if (isCarRotate)
            StartCoroutine(carRotateFunc(car));
        //if (!isCarRotate)
        //    StopCoroutine(carRotateFunc(car));


        if (active_moleFunc)
            StartCoroutine(molePopUpFunc(mole));

        //if (!active_moleFunc)
        //    StopCoroutine(molePopUpFunc(mole));

        if (isCarRotateBack)//차 다시 원상복귀
            StartCoroutine(carRotateBackFunc(car));


        if (isTimerOn)
            time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && isTouch)//말하는 npc랑 닿았고 X를 눌렀다면~
        {
            myLastIndex = textGroup[value].Length;//내 대화 길이 체크하고

            //Action(touchThings.transform.gameObject);
            talkPanel.SetActive(true);
            panelActive = true;
            if (myIndex <= myLastIndex)//내 대화가 끝나기 전까지만 애랑 대화 주고받기
            {
                if (isMyTurn)
                    popMyText(value);
                else
                    popNPCText(value);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && isTouch)//단서 주는 물건들
        {
            if (touchThings.name == "car_pivot")
            {
                car = touchThings;
                isCarRotate = true;
            }

            if (touchThings.name == "mole")//두더지 파묻혀있는거 처음 발견하고 꺼내는 과정
            {
                if (molePopUp)
                    active_moleFunc = true;//mole popup 코루틴 돌릴준비 완료
            }
        }

        switch (check)
        {
            case 1://꽃이 말 걸 차례다
                StartCoroutine(FlowerSay());
                break;

            case -1:
                StopCoroutine(FlowerSay());
                break;

            case 2://꽃이랑 대화 하고 아파트로 가서 specialPlane밟는것까지
                if (touchThings.name == "specialPlane")
                    StartCoroutine(HouseTalk());
                break;


            case -2:
                StopCoroutine(HouseTalk());
                StartCoroutine(CallOtherMap(2));
                break;

            case 3:
                StartCoroutine(doorText());
                break;
            case -3:
                StopCoroutine(doorText());
                hidden.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other) //못움직이게 해야혀
    {//닿았을 대 정보 저장
        if (other.tag == "Things")
        {
            touchThings = other.gameObject;
            isTouch = true;

            switch (other.name)
            {
                case "car_pivot":
                    preCar = other.transform.position;
                    break;

                case "Car":
                    break;
            }

            //    preThing = other.transform.position;

            Debug.Log(other.name);

            talkPanel.SetActive(false);
        }

        else if (other.tag == "NPC")
        {
            touchThings = other.gameObject;
            talkPanel.SetActive(false);
            talkText.text = "";
            objectData objData = other.GetComponent<objectData>();
            value = objData.id; //value값 가져오고
            Talk(objData.id, objData.isNpc);//대화 가져올 준비하고
            isTouch = true;
            isMyTurn = true;

            Debug.Log("value   " + value);
            checkLength();//대화길이 체크하고
        }

        else if (other.name == "specialPlane")
        {
            touchThings = other.gameObject;
            isTouch = true;

            judge.GetComponent<judginScript>().saveQue((int)this.transform.position.x, (int)this.transform.position.y
                      , (int)this.transform.position.z, check, value, myIndex, yourIndex);
        }

        else if (other.name == "hiddenPlane_1")//문을 만났다~
        {
            touchThings = other.gameObject;
            isTouch = true;

            Debug.Log("hiddne");
            check = 3;
        }
        Debug.Log(isTouch);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Things")
        {
            if (other.gameObject.name == "car_pivot")
            {
                isCarRotate = false;
                if (isCarRotateBack)//차 원상복구 시켜야지만 그 위에 화살표 보이게하기
                    arrow_blackCar.SetActive(true);
            }
            yourIndex = 0; myIndex = 0;

        }
        if (other.tag == "NPC" && other.name!="flower")
        {
            if (other.gameObject.name == "mole")
            {
                isCarRotateBack = true;//나갔으니까 두더지 내려가고 차 위치나 회전 원상복귀
                if (isCarRotateBack)//차 원상복구 시켜야지만 그 위에 화살표 보이게하기
                    arrow_blackCar.SetActive(true);
            }
            yourIndex = 0; myIndex = 0;

        }

        else //꽃은 대화 이어가야해서 인덱스 초기화 x
        {
            if (other.name == "flower")
            {
                Debug.Log("내 index" + myIndex);
                Debug.Log("네 index" + yourIndex);
            }

        }
        talkText.text = "";
        Debug.Log("trigger exit "+other.name);
        isTouch = false;


    }

    void Talk(int id, bool isNpc)
    {
        talkManager.GetTalk(id, talkIndex);
    }

    private void generatePlayerText()//내 대화 제작
    {
        //게임 시작 후 바로 나오는 대화
        textGroup.Add(0, new string[] { "(헉.. 헉..) 여긴 어디지? \n처음 오는 곳인데...", "너무 멀리 와버렸어." });//mylast=2
        //1 양이랑 하는 대화
        textGroup.Add(1, new string[]//mylast=3
        { "저기.. ", "길을 잃었어... 여긴 대체 어디야?\n한 눈 팔다가... 여기까지 오게됐어","그렇구나.. 정말 고마워!\n안녕!"});
        //2 두더지랑 대화
        textGroup.Add(2, new string[]
        {
            "앗! 놀라라! ","저기...","..."
        });
        //3 아파트 옆 못난이 꽃
        textGroup.Add(3, new string[]
        {
            "네가 날 불렀니?", "길을 잃었거든", "와, 정말이니? 고마워!!!", "너무한거 아니야? 죽을 뻔 했잖아!",
            "..정말이지?","ㄳ(끝)"
        });
    }

    private void generateNameText()
    {
        //0 나
        nameTextGroup.Add(0, new string[] { "나" });
        //1 양
        nameTextGroup.Add(1, new string[] { "느긋하게 쉬던 양" });
        //2 차 밑 두더지
        nameTextGroup.Add(2, new string[] { "참견하는 두더지" });
        //3 아파트 옆 꽃
        nameTextGroup.Add(3, new string[] { "수상한 꽃", "???" });

    }

    private string GetMyTalk(int id, int myIndex)
    {
        return textGroup[id][myIndex];
    }

    private string GetName(int id, int index)
    {
        return nameTextGroup[id][index];
    }
    private void checkLength()//내 대화 길이 체크
    {
        myLastIndex = textGroup[value].Length;
    }

    private void popMyText(int value)
    { // npc랑 하는 내 대화 띄우기

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
                    nameText.text = GetName(0, 0);
                    changeNameIcon(0);
                    break;
                }
            }
        }
        else
        { //npc랑 대화할 때 내 말들
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
                // if (Input.GetKeyDown(KeyCode.X))
                {
                    if (value == 3 && myIndex== 3 && !twice)//꽃이랑 말할때 ,얘 속마음 말하는걸로 끝남
                    {
                        talkPanel.SetActive(false);
                        panelActive = false;
                        Debug.Log("대화 끝났다는");
                    }
                    else
                    {
                        talkText.text = GetMyTalk(value, myIndex);
                        myIndex++;
                        nameText.text = GetName(0, 0);
                        changeNameIcon(0);
                    }
                        break;
                    
                }
            }
            isMyTurn = false;
        }
    }

    private void popNPCText(int value)
    {
        yourLastIndex = talkManager.CheckLength(value);
        while (true)
        {
            if (yourLastIndex <= yourIndex)
            {
                talkPanel.SetActive(false);
                panelActive = false;
                Debug.Log("대화 긑나서 창 종료");
                yourIndex = 0; myIndex = 0;
                yourLastIndex = 0; myLastIndex = 0;
                if (value == 1)//양이랑 대화가 끝나면
                    check = 1;
                break;
            }
            // if (Input.GetKeyDown(KeyCode.X))
                Debug.Log("X 눌림");
                talkText.text = talkManager.GetTalk(value, yourIndex);//npc index 대화 출력
                yourIndex++;
                isMyTurn = true;
                Debug.Log("현재 두더지 인덱스 " + yourIndex + "끝 인덱스 " + yourLastIndex);
                nameText.text = GetName(value, 0);
                changeNameIcon(value);
                // changeNameIcon(value);
                break;
            }
        }
    

    private void changeNameIcon(int a)//value 인자로 받아야지
    {
        switch (a)
        {
            case 0: // 나잖아
                nameIcon.GetComponent<Image>().sprite = images[0];
                break;
            case 1://양
                nameIcon.GetComponent<Image>().sprite = images[1];
                break;
            case 2://두더지
                nameIcon.GetComponent<Image>().sprite = images[2];
                break;
            case 3://꽃
                nameIcon.GetComponent<Image>().sprite = images[3];
                break;
            case 7://익명 꽃
                nameIcon.GetComponent<Image>().sprite = images[7];
                break;

        }
    }



    private float carRot = 0f;
    private float carPos = 0f;
    IEnumerator carRotateFunc(GameObject car)
    {
        car.transform.position = new Vector3(preCar.x, preCar.y + 2f, preCar.z);

        arrow_blackCar.SetActive(false);
        car.transform.rotation = Quaternion.Slerp(
               car.transform.localRotation, Quaternion.Euler(new Vector3(0, 180, 65f)), Time.time * 0.03f);
        if (car.transform.rotation == Quaternion.Euler(0, 180, 65))
        {
            mole.GetComponent<BoxCollider>().enabled = true;
            car.GetComponent<BoxCollider>().enabled = false;
            molePopUp = true;
            remark_mole.SetActive(true);
            isCarRotate = false;
            Debug.Log("각도 잘 들어옴");
            yield return null;
        }
        // StopCoroutine(carRotateFunc(car));
    }

    IEnumerator carRotateBackFunc(GameObject car)
    {
        myIndex = 0; myLastIndex = 0; yourIndex = 0; yourLastIndex = 0;
        //mole.transform.Translate(new Vector3(0, -0.2f, 0));
        ////yield return new WaitForSecondsRealtime(6f);
        //while(mole.transform.position.y >=-0.2)

        molePopUp = false;
        active_moleFunc = false;
        mole.transform.localPosition = new Vector3(290, -0.2f, 24);
        remark_mole.SetActive(false);
        car.transform.rotation = Quaternion.Slerp(
               car.transform.localRotation, Quaternion.Euler(new Vector3(0, 180, 0)), Time.time * 0.01f);

        if (car.transform.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
        {
            Debug.Log("각도 딱딱 맞춰");
            car.transform.position = preCar;
            mole.GetComponent<BoxCollider>().enabled = false;
            car.GetComponent<BoxCollider>().enabled = true;

            molePopUp = false;
            isCarRotate = false; isCarRotateBack = false;
            arrow_blackCar.SetActive(true);


            yield return null;
        }
        // StopCoroutine(carRotateFunc(car));
    }

    IEnumerator molePopUpFunc(GameObject mole)
    {
        remark_mole.SetActive(false);
        mole.transform.Translate(new Vector3(0, 1f, 0));
        //yield return new WaitForSecondsRealtime(6f);
        if (mole.transform.localPosition.y >= 9f)
        {
            molePopUp = false;
            active_moleFunc = false;
            remark_mole.SetActive(false);
        }
        yield return null;
    }

    IEnumerator FlowerSay()
    {
        isTimerOn = true;
        value = 7;
        if (2f < time && time < 4f)
        {
            talkPanel.SetActive(true);
            panelActive = true;
            nameText.text = GetName(3, 1);
            changeNameIcon(7);

            talkText.text = "야~ 너! 이리와 봐~";
        }
        else if (4f < time && time < 6f)
        {
            talkText.text = "어? 날 부르는건가?";
            nameText.text = GetName(0, 0);
            changeNameIcon(0);
        }
        else if (6f < time && time < 9f)
        {
            talkText.text = "그래 너 ~ \n아파트 옆 쓰레기통으로 와 봐!";
            nameText.text = GetName(3, 1);
            changeNameIcon(7);
        }
        else if (time > 9f)
        {
            isTimerOn = false;
            check = -1;

            talkPanel.SetActive(false);
            panelActive = false;
            talkText.text = "";
            time = 0.0f;

            yield return null;
        }
    }
    //mole=-11.87

    IEnumerator HouseTalk()//아파트에서 혼잣말하는거
    {
        value = 0;
        talkPanel.SetActive(true);
        panelActive = true;
        nameText.text = GetName(0, 0);
        changeNameIcon(0);

        talkText.text = "어?,, 어지러워ㅜ";
        isTimerOn = true;
        if (time > 2f)
        {
            talkPanel.SetActive(false);
            panelActive = false;
            talkText.text = "";
            isTimerOn = false; time = 0.0f;
            check = -2;
            yield return null;
        }
        //}
    }

    public bool saveData = false;
    IEnumerator CallOtherMap(int c)
    {
        audioListener.enabled = false;
        if (c == 2)
        {
            saveData = true;
            AsyncOperation async = SceneManager.LoadSceneAsync("monsterMap");
            DontDestroyOnLoad(judge);
            while (!async.isDone)
                yield return null;
        }
    }

    IEnumerator doorText() //문에 이렇게 쓰여잇네,,! 말하는 함수
    {
        value = 0;
        isTimerOn = true;
        Debug.Log(time);
        if (2f < time && time < 4f)
        {
            talkPanel.SetActive(true);
            panelActive = true;
            nameText.text = GetName(0,0);
            changeNameIcon(0);

            talkText.text = "문에 빛바랜 쪽지가 있네?";
        }
        else if(4f<time && time < 7f)
        {
            talkText.text = "이 문..은 고장..났습니다... \n뭐?????";

        }
        else if (7f < time && time < 10f)
        {
            talkText.text = "하지..만 강에 사는 요리? 아 오리..\n오리를 만나..부탁하면 고칠 수 있습니다!!";

        }
        else if (10f < time && time < 13f)
        {
            talkText.text = "아!! 정말 다행이다!! \n강에 산다는 오리를 찾으러 가야겠어!";

        }
        else if (13f < time)
        {
            talkPanel.SetActive(false);
            panelActive = false;
            talkText.text = "";
            isTimerOn = false; time = 0.0f;
            check = -3;
            yield return null;
        }
    }
}