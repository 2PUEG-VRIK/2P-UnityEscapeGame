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
    private int value=0;//npc�� ���� ������ �� �� �޶��� (npc�� id�� �����ϰ� ����)
    private int myLastIndex;
    private bool panelActive = false;

    Dictionary<int, string[]> textGroup;//�� ��ȭ ������


    private void Start()
    {
        textGroup = new Dictionary<int, string[]>();
        generatePlayerText();
        playerText();
    }

    private void Update()
    {
        if (myIndex == myLastIndex & panelActive)
        {   
            talkPanel.SetActive(false);
            panelActive = false;
            Debug.Log("��ȭ �P���� â ����");
        }
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
        Debug.Log("action ����");
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

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {

                switch (objData.id)
                {
                    //��
                    case 1:
                        myLastIndex = textGroup[objData.id].Length;

                        while (myIndex!=myLastIndex && Input.GetKeyDown(KeyCode.LeftControl)) // �� ��ȭ ���� �ȳ��� ���¿��� ��Ʈ�� ������
                        talkText.text = talkManager.GetTalk(objData.id, index);
                        //talkText.text = GetMyTalk(objData.id, myIndex);
                        //index++;
                        //if (myIndex == myLastIndex)
                        //{
                        //    Debug.Log("�����ϼ��ֻ�");
                        //    talkPanel.SetActive(false);//��ǳ�� ����

                        //}
                        break;


                }
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
        textGroup.Add(0, new string[] { "(��.. ��..) ������! ������!!", "����ִ°ž�..." });
        //���̶� �ϴ� ��ȭ
        textGroup.Add(1, new string[]
        { "�ȳ�?", "Ȥ�� ���� �� ���� ��� ������ �������°� �ô�?","�׷�����.. �Ƿ��߾�! �ȳ�!"});

    }

    private string GetMyTalk(int id, int myIndex)
    {
        return textGroup[id][myIndex];
    }

    private void playerText()//npc�� �ϴ� �� ��ȭ ����
    {
        myLastIndex = textGroup[value].Length;
        talkPanel.SetActive(true);
        panelActive = true;

        while (myIndex == myLastIndex && Input.GetKeyDown(KeyCode.LeftControl))
        { //ȥ�� �پ�ٴϴ� ��Ȳ ����
            talkText.text = GetMyTalk(value, index);
            index++;
        }


    }
}