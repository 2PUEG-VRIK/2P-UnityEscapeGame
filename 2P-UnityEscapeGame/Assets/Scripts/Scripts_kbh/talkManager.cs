using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public int length;
    public int value;//npc의 고유번호
    gameManager3 v;
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();

        GenerateData();
        //CheckLength(value);
    }
    private void Start()
    {
    }
    private void Update()
    {
    }

    void GenerateData()
    {
        //index, string
        //1.양 (가장 처음 만나는 npc)
        talkData.Add(1, new string[]
        {  "넌 누구야? 여긴 무슨일이야?","노란 지붕? 몰라.\n그치만 밖으로 나가는 길은 ~."});

        //2.두더징
        talkData.Add(2, new string[]
         {  "뭐야 내가 더 놀랐어", "종교 안믿는다는"
        });

        //3. 아파트 옆 못난이 꽃
        talkData.Add(3, new string[]
            { "ㅇㅇ뭐찾아?", "나 어디가야하는지 아는데.\n바로 옆 아파트 있지? 거기로 가면 돼", 
                "(ㅎㅎ재밌당)","미안~\n이제 제대로 알려줄게 ㅎㅎ 위치", "ㅇㅇ"
            });


    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }

    public int CheckLength(int value)
    {

        value = GameObject.Find("Man").GetComponent<gameManager3>().value;

        return length = talkData[value].Length;
    }
}