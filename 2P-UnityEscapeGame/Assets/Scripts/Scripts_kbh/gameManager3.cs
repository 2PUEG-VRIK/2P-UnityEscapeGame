using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//0 �� ȥ��
//1 ��
//2 �δ���
public class gameManager3 : MonoBehaviour
{
    public talkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction = false;
    public int talkIndex;
    private int yourIndex;//npc��ȭ �ε���
    private int myIndex;//�� ��ȭ �ε���
    public int value;//npc�� ���� ������ �� �� �޶��� (npc�� id�� �����ϰ� ����)
    private int myLastIndex=-1;
    private int yourLastIndex;
    private bool panelActive = false;
    private bool isMyTurn=true;//���� ��ȭ�� ���ʳ�~
    private bool first ;//ó�� ���� �� �Ҷ��� ���̴� ����
    private bool firstTouch = false;//npc�̶� �ݶ��̴� ó�� ������ ���̴� ����. ���� ���� ���ؾ��� ��
    Dictionary<int, string[]> textGroup;//�� ��ȭ ������
    private bool isCarRotate;
    private bool isCarRotateBack;

    GameObject car;
    GameObject mole;
    private bool molePopUp;
    private bool active_moleFunc;//update�Լ����� �ڷ�ƾ ������
    GameObject remark_mole;//�ֵ� �Ӹ� ���� ����ǥ(����ġ ���� �߿��� �ܼ�)
    GameObject arrow_blackCar;
    new Vector3 preCar;//�� ���� ��ǥ
    new Vector3 preThing;//���� ���� ��ǥ

    private float time;
    private bool isTimerOn;

    private void Start()
    {
        yourIndex = 0; myIndex = 0;
        value = 0; myLastIndex = -1;
        first = true;//���� ���� �� �����ϸ鼭 ���� �����ؾ��ϴϱ�
        firstTouch = false;//���� �����̶� �� ���� ���´ϱ�
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

        if(isCarRotateBack)//�� �ٽ� ���󺹱�
            StartCoroutine(carRotateBackFunc(car));

        if (isTimerOn)
            time += Time.deltaTime;
        //}

    }

    private void OnTriggerEnter(Collider other) //�������̰� �ؾ���
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
                Debug.Log("�δ��� �ݶ� �ȿ� ���ͼ� my turn true��");
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
            if (Input.GetKeyDown(KeyCode.X))//��ȭ���� npc
            {
                Action(other.transform.gameObject);
            }

            else if (Input.GetKeyDown(KeyCode.LeftAlt))//�ܼ� �ִ� ���ǵ�
            {
                if (other.gameObject.name == "car_pivot")
                {
                    car = other.gameObject;
                    isCarRotate = true;
                    //  if (Input.GetKeyDown(KeyCode.LeftShift))
                    //StartCoroutine(carRotate(other.gameObject));
                }

                if (other.gameObject.name == "mole")//�δ��� �Ĺ����ִ°� ó�� �߰��ϰ� ������ ����
                    if (molePopUp)
                        active_moleFunc = true;//mole popup �ڷ�ƾ �����غ� �Ϸ�
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
            myLastIndex = textGroup[objData.id].Length;//�� ��ȭ ���� üũ�ϰ�
            firstTouch = false;

            if (myIndex <= myLastIndex)//�� ��ȭ�� ������ �������� �ֶ� ��ȭ �ְ�ޱ�
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
                isCarRotateBack = true;//�������ϱ� �δ��� �������� �� ��ġ�� ȸ�� ���󺹱�
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

    private void generatePlayerText()//�� ��ȭ ����
    {
        //���� ���� �� �ٷ� ������ ��ȭ
        textGroup.Add(0, new string[] { "(��.. ��..) ������! ������!!", "����ִ°ž�..." });//mylast=2
        //���̶� �ϴ� ��ȭ
        textGroup.Add(1, new string[]//mylast=3
        { "�ȳ�?", "���� �εξ�. Ȥ�� ����\n��� ������ �������°� ���ô�?","�׷�����.. �Ƿ��߾�! �ȳ�!"});
        //�δ����� ��ȭ
        textGroup.Add(2, new string[]
        {
            "��(1)","��(3)","��(��)"
        });

        textGroup.Add(3, new string[]
        {
            "��(1)", "��(3)", "��(5)"
        });
    }

    private string GetMyTalk(int id, int myIndex)
    {
        return textGroup[id][myIndex];
    }

    private void checkLength()//�� ��ȭ ���� üũ
    {
        myLastIndex = textGroup[value].Length;

        talkPanel.SetActive(true);
        panelActive = true;
    }

    private void popMyText(int value) { // npc�� �ϴ� �� ��ȭ ����

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
                    break;

                }
            }
        }

        else
        { //npc�� ��ȭ�� �� �� ����
          // if (myLastIndex <= myIndex) //�� ��ȭ ���� ����
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
                if (Input.GetKeyDown(KeyCode.X))
                {
                    talkText.text = GetMyTalk(value, myIndex);
                    myIndex++;
                    if (Input.GetKeyDown(KeyCode.X)) Debug.Log("X ����");
                    Debug.Log("���� �� �ε��� " + myIndex + "    �� �ε��� " + myLastIndex);
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
                Debug.Log("��ȭ �P���� â ����");
                yourIndex = 0; myIndex = 0;
                yourLastIndex = 0; myLastIndex = 0;
                break;
            }
            if (Input.GetKeyDown(KeyCode.X)) 
            {
                Debug.Log("X ����");
                talkText.text = talkManager.GetTalk(value, yourIndex);//npc index ��ȭ ���
                yourIndex++;
                isMyTurn = true;
                Debug.Log("���� �δ��� �ε��� " + yourIndex + "�� �ε��� " + yourLastIndex);
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
        talkText.text = "��! ��! �̸��� ��!";

        isTimerOn = true;
        if (time > 2.5f)
            talkText.text = "��? �� �θ��°ǰ�?";
        if (time > 5f)
            talkText.text = "�׷� �� ~";


    }
  //mole=-11.87

}