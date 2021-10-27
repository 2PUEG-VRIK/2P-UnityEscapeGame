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
    GameObject car;
    GameObject mole;
    private bool molePopUp;
    private bool active_moleFunc;//update�Լ����� �ڷ�ƾ ������
    GameObject remark;//�ֵ� �Ӹ� ���� ����ǥ(����ġ ���� �߿��� �ܼ�)

    private void Start()
    {
        yourIndex = 0; myIndex = 0;
        value = 0; myLastIndex = -1;
        first = true;//���� ���� �� �����ϸ鼭 ���� �����ؾ��ϴϱ�
        firstTouch = false;//���� �����̶� �� ���� ���´ϱ�
        isCarRotate = false;
        textGroup = new Dictionary<int, string[]>();
        molePopUp = false;
        active_moleFunc = false;
        remark = GameObject.Find("npcArrow").transform.GetChild(2).gameObject;
        mole = GameObject.Find("mole");
        Debug.Log(remark.name);
        generatePlayerText();
        checkLength();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isMyTurn)
            if (first) popMyText(value);

        if (isCarRotate)
            StartCoroutine(carRotateFunc(car));
        if (!isCarRotate)
            StopCoroutine(carRotateFunc(car));
        if (active_moleFunc)
        {
            Debug.Log("������Ʈ�Լ� ����");
            StartCoroutine(molePopUpFunc(mole));

        }
        if (!active_moleFunc)
            StopCoroutine(molePopUpFunc(mole));

    //}

}

private void OnTriggerEnter(Collider other) //�������̰� �ؾ���
    {
        if (other.tag == "Things" )
        {
            isMyTurn = true;
            checkLength();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Things")
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))//��ȭ���� npc
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
                isCarRotate = false;
            yourIndex = 0; 
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
        { "�ȳ�?", "Ȥ�� ���� �� ���� ��� ������\n�������°� �ô�?","�׷�����.. �Ƿ��߾�! �ȳ�!"});
        //�δ����� ��ȭ
        textGroup.Add(2, new string[]
        {
            "�δ���~~!!"
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

        if ( value == 0)
        { //ȥ�� �پ�ٴϴ� ��Ȳ ����
            if (myIndex == myLastIndex)//��ȭ�� ���� �����ϸ� 
            {
                talkPanel.SetActive(false);
                panelActive = false;
                yourIndex = 0; myIndex = 0;
                isMyTurn = false;
                first = false;
            }
            else
            {
                talkText.text = GetMyTalk(value, myIndex);
                myIndex++;
                Debug.Log(myIndex);
            }
        }

        else
        { //npc�� ��ȭ�� �� �� ����
            if(myLastIndex==myIndex)//�� ��ȭ ���� ����
            {
                talkPanel.SetActive(false);
                panelActive = false;
                Debug.Log("��ȭ �P���� â ����");
                yourIndex = 0; myIndex = 0;
                yourLastIndex = 0; myLastIndex = 0;
            }

            else
            {
                talkText.text = GetMyTalk(value, myIndex);
                myIndex++;
            }
            isMyTurn = false;
        }
    }

    private void popNPCText(int value)
    {
        switch (value)
        {
            //��
            case 1:
                {
                    yourLastIndex = talkManager.CheckLength(value);
                   
                    if (yourIndex < yourLastIndex)
                    {
                        talkText.text = talkManager.GetTalk(value, yourIndex);//npc index ��ȭ ���
                        yourIndex++;
                        isMyTurn = true;
                    }
                    else //�� ��ȭ ��~ �� ���ֹ���
                    {
                        talkPanel.SetActive(false);
                        panelActive = false;
                        Debug.Log("��ȭ �P���� â ����");
                        yourIndex = 0; myIndex = 0;
                        yourLastIndex = 0; myLastIndex = 0;
                    }
                }
                break;
        }
    }

    IEnumerator carRotateFunc(GameObject car)
    {
        //236.7, -9,-91.2
        car.transform.rotation = Quaternion.Slerp(
               car.transform.localRotation, Quaternion.Euler(new Vector3(0, 180, 80f)), Time.time * 0.02f);
        
        yield return new WaitForSecondsRealtime(1f);
        //if (car.transform.rotation == Quaternion.Euler(new Vector3(0, 180, 50f)))
        mole.GetComponent<BoxCollider>().enabled = true;
        car.GetComponent<BoxCollider>().enabled = false;
        molePopUp = true;
        remark.SetActive(true);
        isCarRotate = false;

        // StopCoroutine(carRotateFunc(car));
    }

    IEnumerator molePopUpFunc(GameObject mole)
    {
        Debug.Log("�δ����ڷ�ƾ");
        Debug.Log(mole.transform.localPosition.y);

        mole.transform.Translate(new Vector3(0, 1f, 0));
        //yield return new WaitForSecondsRealtime(6f);
        if (mole.transform.localPosition.y >= -11f)
        { molePopUp = false;
            active_moleFunc = false;
        }

        yield return null;
    }
  //mole=-11.87

}