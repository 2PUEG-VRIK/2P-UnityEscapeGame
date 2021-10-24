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
    private int index;//��ȭ �ε���
    private int myIndex;//�� ��ȭ �ε���
    private int value;//npc�� ���� ������ �� �� �޶��� (npc�� id�� �����ϰ� ����)
    private int myLastIndex=-1;
    private bool panelActive = false;
    private bool isMyTurn=true;//���� ��ȭ�� ���ʳ�~

    Dictionary<int, string[]> textGroup;//�� ��ȭ ������

    private void Start()
    {
        index = 0; myIndex = 0;
        value = 0; myLastIndex = -1; 

        textGroup = new Dictionary<int, string[]>();
        generatePlayerText();
        checkLength();
        //popMyText(value);
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.LeftControl) && isMyTurn) 
           popMyText(value);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Things")
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
                Action(other.transform.gameObject);
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
         
            if (Input.GetKeyDown(KeyCode.LeftControl))
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
            index = 0;
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
                Debug.Log("��ȭ �P���� â ����");
                index = 0; myIndex = 0;
                isMyTurn = false;
            }
            else
            {
                talkText.text = GetMyTalk(value, myIndex);
                myIndex++;
                Debug.Log(myIndex);
            }
        }

        else
        {
            if(myLastIndex==myIndex)//�� ��ȭ ���� ����
            {
                talkPanel.SetActive(false);
                panelActive = false;
                Debug.Log("��ȭ �P���� â ����");
                index = 0; myIndex = 0;
                isMyTurn = false;
            }

            else
            {
                talkText.text = GetMyTalk(value, myIndex);
                myIndex++;
                Debug.Log(myIndex);
                isMyTurn = true;
            }
        }
    }

    private void popNPCText(int value)
    {
        switch (value)
        {
            //��
            case 1:
                //if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    talkText.text = talkManager.GetTalk(value, index);//npc index ��ȭ ���
                    index++;
                    Debug.Log(talkText.text);
                    isMyTurn = true;
                }
                break;
        }
    }

}