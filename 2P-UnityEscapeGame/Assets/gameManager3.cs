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
    private int value=0;//npc에 따라 나가는 내 말 달라짐 (npc의 id와 동일하게 하자)
    private int myLastIndex;
    private bool panelActive = false;

    Dictionary<int, string[]> textGroup;//내 대화 뭉텅이


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
            Debug.Log("대화 긑나서 창 종료");
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
        Debug.Log("action 들어옴");
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
                    //양
                    case 1:
                        myLastIndex = textGroup[objData.id].Length;

                        while (myIndex!=myLastIndex && Input.GetKeyDown(KeyCode.LeftControl)) // 내 대화 아직 안끝난 상태에서 컨트롤 누르면
                        talkText.text = talkManager.GetTalk(objData.id, index);
                        //talkText.text = GetMyTalk(objData.id, myIndex);
                        //index++;
                        //if (myIndex == myLastIndex)
                        //{
                        //    Debug.Log("움직일수있삼");
                        //    talkPanel.SetActive(false);//말풍선 접어

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

    private void generatePlayerText()//내 대화 제작
    {
        //게임 시작 후 바로 나오는 대화
        textGroup.Add(0, new string[] { "(헉.. 헉..) 누렁아! 누렁아!!", "어디있는거야..." });
        //양이랑 하는 대화
        textGroup.Add(1, new string[]
        { "안녕?", "혹시 여기 털 색이 노란 강아지 지나가는거 봤니?","그렇구나.. 실례했어! 안녕!"});

    }

    private string GetMyTalk(int id, int myIndex)
    {
        return textGroup[id][myIndex];
    }

    private void playerText()//npc랑 하는 내 대화 띄우기
    {
        myLastIndex = textGroup[value].Length;
        talkPanel.SetActive(true);
        panelActive = true;

        while (myIndex == myLastIndex && Input.GetKeyDown(KeyCode.LeftControl))
        { //혼자 뛰어다니는 상황 설명
            talkText.text = GetMyTalk(value, index);
            index++;
        }


    }
}