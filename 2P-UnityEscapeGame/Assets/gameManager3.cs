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
    private int index;//대화 인덱스
    private int myIndex;//내 대화 인덱스
    private int value;//npc에 따라 나가는 내 말 달라짐 (npc의 id와 동일하게 하자)
    private int myLastIndex=-1;
    private bool panelActive = false;
    private bool isMyTurn=true;//내가 대화할 차례냐~

    Dictionary<int, string[]> textGroup;//내 대화 뭉텅이

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
            myLastIndex = textGroup[objData.id].Length;//내 대화 길이 체크하고
         
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

    private void generatePlayerText()//내 대화 제작
    {
        //게임 시작 후 바로 나오는 대화
        textGroup.Add(0, new string[] { "(헉.. 헉..) 누렁아! 누렁아!!", "어디있는거야..." });//mylast=2
        //양이랑 하는 대화
        textGroup.Add(1, new string[]//mylast=3
        { "안녕?", "혹시 여기 털 색이 노란 강아지\n지나가는거 봤니?","그렇구나.. 실례했어! 안녕!"});
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

        if ( value == 0)
        { //혼자 뛰어다니는 상황 설명
            if (myIndex == myLastIndex)//대화의 끝에 도달하면 
            {
                talkPanel.SetActive(false);
                panelActive = false;
                Debug.Log("대화 긑나서 창 종료");
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
            if(myLastIndex==myIndex)//내 대화 끝에 도달
            {
                talkPanel.SetActive(false);
                panelActive = false;
                Debug.Log("대화 긑나서 창 종료");
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
            //양
            case 1:
                //if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    talkText.text = talkManager.GetTalk(value, index);//npc index 대화 출력
                    index++;
                    Debug.Log(talkText.text);
                    isMyTurn = true;
                }
                break;
        }
    }

}