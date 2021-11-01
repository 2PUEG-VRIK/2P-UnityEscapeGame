using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// npc ������ ����
/// �� -> ��ȭ������ ���� �θ� -> ������ �� -> �ӾƼ� ����Ʈ �� -> monsterMap ->������ ������ ��
/// -> ����� �˷��� �� ������ �� -> ���� ���峪 ���� ->
/// </summary>
//value
//0 �� ȥ��
//1 ��
//2 �δ���
//3 ��

/*check(�ʱⰪ 0)
1- �� ȸ�� ���󺹱� ������ ���� �� �� �� �ʿ��ؼ�
-1- ���� �� �Ŵ°� ������ 

2 (�� �� ���) ����Ʈ�� ���� specialPlane ���->monsterMap���� �̵�����.
-2 houseTalk�ڷ�ƾ �ߴܽ�Ű�� ����. monsterMap���� ����� ���ƿ��� check=-2

3 ���տ� �ִ� ���� �߰�! ���� ã���� ��

*/
public class gameManager3 : MonoBehaviour
{
    public talkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    private GameObject scanObject;

    public GameObject namePanel;//���� ���ϴ��� �ߴ� �г�
    public Image nameIcon;//���ϴ� �� ������ �ߴ� ��~
    public Text nameText;//�̸� �ߴ� text

    //public bool isAction = false;
    public int talkIndex;
    public int yourIndex;//npc��ȭ �ε���
    public int myIndex;//�� ��ȭ �ε���
    public int value;//npc�� ���� ������ �� �� �޶��� (npc�� id�� �����ϰ� ����)
    private int myLastIndex = -1;
    private int yourLastIndex;
    private bool panelActive = false;
    private bool isMyTurn = true;//���� ��ȭ�� ���ʳ�~
    private bool first;//ó�� ���� �� �Ҷ��� ���̴� ����
    private bool firstTouch = false;//npc�̶� �ݶ��̴� ó�� ������ ���̴� ����. ���� ���� ���ؾ��� ��
    private bool isCarRotate;
    private bool isCarRotateBack;
    Dictionary<int, string[]> textGroup;//�� ��ȭ ������
    Dictionary<int, string[]> nameTextGroup;//���ϴ� ����� �̸� ������


    GameObject car;
    GameObject mole;
    private bool molePopUp;
    private bool active_moleFunc;//update�Լ����� �ڷ�ƾ ������
    GameObject remark_mole;//�ֵ� �Ӹ� ���� ����ǥ(����ġ ���� �߿��� �ܼ�)
    GameObject arrow_blackCar;
    Vector3 preCar;//�� ���� ��ǥ
    Vector3 preThing;//���� ���� ��ǥ

    private float time;//�� ���� �ð� on
    private bool isTimerOn;
    public int check;//�������� ���� ����

    public Sprite[] images;
    private GameObject touchThings;//���� ��ü
    private bool isTouch;//�ݶ��̴� ����� �� true���� ����

    //�� �� �̵�
    AudioListener audioListener;//�̵��� �� ���� ���� ����� ������ ����
    saveManagerScript data;
    //public static List<int> l1 = new List<int>();//���� ���� �� ���� ��� ����
    //static List<int> l2= new List<int>();
    public bool alreadyCame = false;//�� �ѹ� ���ٿ°���~
    //GameObject saveM;
    GameObject judge;//�� �̵� �ߴ��� �Ǻ�
    judginScript judgeSc;
    private bool twice = false;//�� �� �̵��ϸ� �� �ķ� �� true


    //���� �˷��ش�� ���� ��
    GameObject exitDoor;
    GameObject hidden;//door �� hiddenPlane

    private void Start()
    {
        yourIndex = 0; myIndex = 0;
        value = 0; myLastIndex = -1;
        first = true;//���� ���� �� �����ϸ鼭 ���� �����ؾ��ϴϱ�
        firstTouch = false;//���� �����̶� �� ���� ���´ϱ�
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

        if (GameObject.Find("judging").GetComponent<judginScript>().yes)//���ٿ�
        {
            Debug.Log("judging");
            GameObject.Find("specialPlane").SetActive(false);
            nameText.text = GetName(0, 0);
            changeNameIcon(0);
            talkText.text = "���� �����߰ھ�";
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

        if (isCarRotateBack)//�� �ٽ� ���󺹱�
            StartCoroutine(carRotateBackFunc(car));


        if (isTimerOn)
            time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && isTouch)//���ϴ� npc�� ��Ұ� X�� �����ٸ�~
        {
            myLastIndex = textGroup[value].Length;//�� ��ȭ ���� üũ�ϰ�

            //Action(touchThings.transform.gameObject);
            talkPanel.SetActive(true);
            panelActive = true;
            if (myIndex <= myLastIndex)//�� ��ȭ�� ������ �������� �ֶ� ��ȭ �ְ�ޱ�
            {
                if (isMyTurn)
                    popMyText(value);
                else
                    popNPCText(value);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && isTouch)//�ܼ� �ִ� ���ǵ�
        {
            if (touchThings.name == "car_pivot")
            {
                car = touchThings;
                isCarRotate = true;
            }

            if (touchThings.name == "mole")//�δ��� �Ĺ����ִ°� ó�� �߰��ϰ� ������ ����
            {
                if (molePopUp)
                    active_moleFunc = true;//mole popup �ڷ�ƾ �����غ� �Ϸ�
            }
        }

        switch (check)
        {
            case 1://���� �� �� ���ʴ�
                StartCoroutine(FlowerSay());
                break;

            case -1:
                StopCoroutine(FlowerSay());
                break;

            case 2://���̶� ��ȭ �ϰ� ����Ʈ�� ���� specialPlane��°ͱ���
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

    private void OnTriggerEnter(Collider other) //�������̰� �ؾ���
    {//����� �� ���� ����
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
            value = objData.id; //value�� ��������
            Talk(objData.id, objData.isNpc);//��ȭ ������ �غ��ϰ�
            isTouch = true;
            isMyTurn = true;

            Debug.Log("value   " + value);
            checkLength();//��ȭ���� üũ�ϰ�
        }

        else if (other.name == "specialPlane")
        {
            touchThings = other.gameObject;
            isTouch = true;

            judge.GetComponent<judginScript>().saveQue((int)this.transform.position.x, (int)this.transform.position.y
                      , (int)this.transform.position.z, check, value, myIndex, yourIndex);
        }

        else if (other.name == "hiddenPlane_1")//���� ������~
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
                if (isCarRotateBack)//�� ���󺹱� ���Ѿ����� �� ���� ȭ��ǥ ���̰��ϱ�
                    arrow_blackCar.SetActive(true);
            }
            yourIndex = 0; myIndex = 0;

        }
        if (other.tag == "NPC" && other.name!="flower")
        {
            if (other.gameObject.name == "mole")
            {
                isCarRotateBack = true;//�������ϱ� �δ��� �������� �� ��ġ�� ȸ�� ���󺹱�
                if (isCarRotateBack)//�� ���󺹱� ���Ѿ����� �� ���� ȭ��ǥ ���̰��ϱ�
                    arrow_blackCar.SetActive(true);
            }
            yourIndex = 0; myIndex = 0;

        }

        else //���� ��ȭ �̾���ؼ� �ε��� �ʱ�ȭ x
        {
            if (other.name == "flower")
            {
                Debug.Log("�� index" + myIndex);
                Debug.Log("�� index" + yourIndex);
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

    private void generatePlayerText()//�� ��ȭ ����
    {
        //���� ���� �� �ٷ� ������ ��ȭ
        textGroup.Add(0, new string[] { "(��.. ��..) ���� �����? \nó�� ���� ���ε�...", "�ʹ� �ָ� �͹��Ⱦ�." });//mylast=2
        //1 ���̶� �ϴ� ��ȭ
        textGroup.Add(1, new string[]//mylast=3
        { "����.. ", "���� �Ҿ���... ���� ��ü ����?\n�� �� �ȴٰ�... ������� ���Եƾ�","�׷�����.. ���� ����!\n�ȳ�!"});
        //2 �δ����� ��ȭ
        textGroup.Add(2, new string[]
        {
            "��! ����! ","����...","..."
        });
        //3 ����Ʈ �� ������ ��
        textGroup.Add(3, new string[]
        {
            "�װ� �� �ҷ���?", "���� �Ҿ��ŵ�", "��, �����̴�? ����!!!", "�ʹ��Ѱ� �ƴϾ�? ���� �� ���ݾ�!",
            "..��������?","��(��)"
        });
    }

    private void generateNameText()
    {
        //0 ��
        nameTextGroup.Add(0, new string[] { "��" });
        //1 ��
        nameTextGroup.Add(1, new string[] { "�����ϰ� ���� ��" });
        //2 �� �� �δ���
        nameTextGroup.Add(2, new string[] { "�����ϴ� �δ���" });
        //3 ����Ʈ �� ��
        nameTextGroup.Add(3, new string[] { "������ ��", "???" });

    }

    private string GetMyTalk(int id, int myIndex)
    {
        return textGroup[id][myIndex];
    }

    private string GetName(int id, int index)
    {
        return nameTextGroup[id][index];
    }
    private void checkLength()//�� ��ȭ ���� üũ
    {
        myLastIndex = textGroup[value].Length;
    }

    private void popMyText(int value)
    { // npc�� �ϴ� �� ��ȭ ����

        if (value == 0)
        { //ȥ�� �پ�ٴϴ� ��Ȳ ����
          // if (myLastIndex <= myIndex) //��ȭ�� ���� �����ϸ� 
            while (true)
            {
                if (myLastIndex <= myIndex)
                {
                    talkPanel.SetActive(false);
                    panelActive = false;
                    yourIndex = 0; myIndex = 0;
                    first = false;
                    Debug.Log("��ȭ �����ٴ�");
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
        { //npc�� ��ȭ�� �� �� ����
            while (true)
            {
                if (myLastIndex <= myIndex)
                {
                    talkPanel.SetActive(false);
                    panelActive = false;
                    Debug.Log("��ȭ �����ٴ�");

                    yourIndex = 0; myIndex = 0;
                    yourLastIndex = 0; myLastIndex = 0;
                   


                    break;
                }
                // if (Input.GetKeyDown(KeyCode.X))
                {
                    if (value == 3 && myIndex== 3 && !twice)//���̶� ���Ҷ� ,�� �Ӹ��� ���ϴ°ɷ� ����
                    {
                        talkPanel.SetActive(false);
                        panelActive = false;
                        Debug.Log("��ȭ �����ٴ�");
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
                Debug.Log("��ȭ �P���� â ����");
                yourIndex = 0; myIndex = 0;
                yourLastIndex = 0; myLastIndex = 0;
                if (value == 1)//���̶� ��ȭ�� ������
                    check = 1;
                break;
            }
            // if (Input.GetKeyDown(KeyCode.X))
                Debug.Log("X ����");
                talkText.text = talkManager.GetTalk(value, yourIndex);//npc index ��ȭ ���
                yourIndex++;
                isMyTurn = true;
                Debug.Log("���� �δ��� �ε��� " + yourIndex + "�� �ε��� " + yourLastIndex);
                nameText.text = GetName(value, 0);
                changeNameIcon(value);
                // changeNameIcon(value);
                break;
            }
        }
    

    private void changeNameIcon(int a)//value ���ڷ� �޾ƾ���
    {
        switch (a)
        {
            case 0: // ���ݾ�
                nameIcon.GetComponent<Image>().sprite = images[0];
                break;
            case 1://��
                nameIcon.GetComponent<Image>().sprite = images[1];
                break;
            case 2://�δ���
                nameIcon.GetComponent<Image>().sprite = images[2];
                break;
            case 3://��
                nameIcon.GetComponent<Image>().sprite = images[3];
                break;
            case 7://�͸� ��
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
            Debug.Log("���� �� ����");
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
            Debug.Log("���� ���� ����");
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

            talkText.text = "��~ ��! �̸��� ��~";
        }
        else if (4f < time && time < 6f)
        {
            talkText.text = "��? �� �θ��°ǰ�?";
            nameText.text = GetName(0, 0);
            changeNameIcon(0);
        }
        else if (6f < time && time < 9f)
        {
            talkText.text = "�׷� �� ~ \n����Ʈ �� ������������ �� ��!";
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

    IEnumerator HouseTalk()//����Ʈ���� ȥ�㸻�ϴ°�
    {
        value = 0;
        talkPanel.SetActive(true);
        panelActive = true;
        nameText.text = GetName(0, 0);
        changeNameIcon(0);

        talkText.text = "��?,, ����������";
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

    IEnumerator doorText() //���� �̷��� �����ճ�,,! ���ϴ� �Լ�
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

            talkText.text = "���� ���ٷ� ������ �ֳ�?";
        }
        else if(4f<time && time < 7f)
        {
            talkText.text = "�� ��..�� ����..�����ϴ�... \n��?????";

        }
        else if (7f < time && time < 10f)
        {
            talkText.text = "����..�� ���� ��� �丮? �� ����..\n������ ����..��Ź�ϸ� ��ĥ �� �ֽ��ϴ�!!";

        }
        else if (10f < time && time < 13f)
        {
            talkText.text = "��!! ���� �����̴�!! \n���� ��ٴ� ������ ã���� ���߰ھ�!";

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