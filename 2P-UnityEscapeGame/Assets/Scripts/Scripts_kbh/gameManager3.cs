using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//0 �� ȥ��
//1 ��
//2 �δ���


/*check(�ʱⰪ 0)
1- �� ȸ�� ���󺹱� ������ ���� �� �� �� �ʿ��ؼ�
-1- ���� �� �Ŵ°� ������ 

2 �� �� ��� ����Ʈ�� ���� specialPlane ���

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
    private int yourIndex;//npc��ȭ �ε���
    private int myIndex;//�� ��ȭ �ε���
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
    private int check;//�������� ���� ����

    public Sprite[] images;
    private GameObject touchThings;//���� ��ü
    private bool isTouch;//�ݶ��̴� ����� �� true���� ����


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
        //sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        isTimerOn = false;
        isTouch = false;

        check = 0;
        time = 0f;

        generatePlayerText();
        generateNameText();
        checkLength();
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

                case "specialPlane":
                    check = 2;
                    break;
            }
            
            //    preThing = other.transform.position;
          
            Debug.Log(other.name);

            talkPanel.SetActive(false);
        }

        else if (other.tag == "NPC")
        {
            touchThings = other.gameObject;

            objectData objData = other.GetComponent<objectData>();
            value = objData.id; //value�� ��������
            Talk(objData.id, objData.isNpc);//��ȭ ������ �غ��ϰ�
            isTouch = true;
            isMyTurn = true;
           
            Debug.Log("value   " + value);
            checkLength();//��ȭ���� üũ�ϰ�
        }
    }

    private void OnTriggerStay(Collider other)
    {
       
    }

    //public void Action(GameObject scanObj)
    //{
    //    //  playerText();
    //    if (isAction)
    //        isAction = false;

    //    else //Action==false
    //    {
    //        //scanObject = scanObj;
    //        //objectData objData = scanObject.GetComponent<objectData>();
    //        isAction = true;

    //        //Talk(objData.id, objData.isNpc);
    //        //talkPanel.SetActive(true);
    //        //panelActive = true;
    //        //value = objData.id;
    //        //myLastIndex = textGroup[objData.id].Length;//�� ��ȭ ���� üũ�ϰ�
    //       // firstTouch = false;

    //        //if (myIndex <= myLastIndex)//�� ��ȭ�� ������ �������� �ֶ� ��ȭ �ְ�ޱ�
    //        //{
    //        //    if (isMyTurn)
    //        //        popMyText(value);
    //        //    else
    //        //        popNPCText(value);
    //        //}
    //    }
    //}


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
        }
        if (other.tag == "NPC") { 
            if (other.gameObject.name == "mole")
            {
                isCarRotateBack = true;//�������ϱ� �δ��� �������� �� ��ġ�� ȸ�� ���󺹱�
                if (isCarRotateBack)//�� ���󺹱� ���Ѿ����� �� ���� ȭ��ǥ ���̰��ϱ�
                    arrow_blackCar.SetActive(true);
            }
            talkText.text = "";
        }
        yourIndex = 0; myIndex = 0;
        isTouch = false;

    }

    void Talk(int id, bool isNpc)
    {
        talkManager.GetTalk(id, talkIndex);
    }

    private void generatePlayerText()//�� ��ȭ ����
    {
        //���� ���� �� �ٷ� ������ ��ȭ
        textGroup.Add(0, new string[] { "(��.. ��..) ������! ������!!", "����ִ°ž�..." });//mylast=2
        //1 ���̶� �ϴ� ��ȭ
        textGroup.Add(1, new string[]//mylast=3
        { "�ȳ�?", "���� �εξ�. Ȥ�� ����\n��� ������ �������°� ���ô�?","�׷�����.. �Ƿ��߾�! �ȳ�!"});
        //2 �δ����� ��ȭ
        textGroup.Add(2, new string[]
        {
            "��! ����! \n�� ������?","���㸦 �Ҿ���Ⱦ� �̤�","��!! ���� ����!!"
        });
        //3 ����Ʈ �� ������ ��
        textGroup.Add(3, new string[]
        {
            "�װ� �� �ҷ���?", "��(3)", "��(5)"
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
                    talkText.text = GetMyTalk(value, myIndex);
                    myIndex++;
                    nameText.text = GetName(0, 0);
                    changeNameIcon(0);

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
                break;
            }
           // if (Input.GetKeyDown(KeyCode.X))
            {
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
        }
    }
    private float carRot = 0f;
    private float carPos = 0f;
    IEnumerator carRotateFunc(GameObject car)
    {
        //car.transform.position = new Vector3(car.transform.position.x, 
        //    car.transform.position.y + 0.3f, car.transform.position.z);
        //while (true)
        //{
        //    carPos += 0.07f * Time.deltaTime;
        //    car.transform.position = new Vector3(car.transform.position.x, car.transform.position.y + carPos,
        //        car.transform.position.z);
        //    if (carPos > 2)
        //        break;
        //}

        car.transform.position = new Vector3(preCar.x, preCar.y + 2f, preCar.z);

        arrow_blackCar.SetActive(false);
        car.transform.rotation = Quaternion.Slerp(
               car.transform.localRotation, Quaternion.Euler(new Vector3(0, 180, 65f)), Time.time * 0.03f);

        //while (true)
        //{ 
        //    carRot += 0.001f * Time.deltaTime;
        //    car.transform.rotation = Quaternion.Euler(0, 180, carRot);
        //    if (carRot >= 75f)
        //        break;
        //}
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

            check = 1;

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
        if (2f < time && time < 5f)
        {
            talkPanel.SetActive(true);
            panelActive = true;
            nameText.text = GetName(3, 1);
            changeNameIcon(3);

            talkText.text = "��! ��! �̸��� ��!";
        }
        else if (5f < time && time < 7f)
        {
            talkText.text = "��? �� �θ��°ǰ�?";
            nameText.text = GetName(0,0);
            changeNameIcon(0);
        }
        else if (7f < time && time < 10f)
        {
            talkText.text = "�׷� �� ~ \n����Ʈ �� ������������ �ͺ�!";
            nameText.text = GetName(3, 1);
            changeNameIcon(3);
        }
        else if (time > 10f)
        {
            isTimerOn = false;
            check = -1;

            talkPanel.SetActive(false);
            panelActive = false;
            talkText.text = "";

            yield return null;
        }
    }
    //mole=-11.87

}