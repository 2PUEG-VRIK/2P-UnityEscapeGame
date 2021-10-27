using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        yourIndex = 0; myIndex = 0;
        value = 0; myLastIndex = -1;
        first = true;//���� ���� �� �����ϸ鼭 ���� �����ؾ��ϴϱ�
        firstTouch = false;//���� �����̶� �� ���� ���´ϱ�
        textGroup = new Dictionary<int, string[]>();
        generatePlayerText();
        checkLength();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isMyTurn)
            if(first) popMyText(value); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Things")
        { 
            isMyTurn = true;
            checkLength();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Things")
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Action(other.transform.gameObject); 
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
            yourIndex = 0;
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

}