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
        //1.양 -처음
        talkData.Add(1, new string[]
        {  "넌 누구야? 여긴 무슨일이야?","노란 지붕? 몰라.\n여기 사는 동물중에선 두더지가 가장 똑똑하단말야! 난 몰라!"});

        //2.두더징 -세번째
        talkData.Add(2, new string[]
         {  "뭐야! 내가 더 놀랐어", "그보다 너, 밖으로 나가는 문을 고치고 싶은거지?","땅 속에서도 다 울려\n흠흠, 나가고싶으면 저 가로등으로 걸어들어가",
         "정말이야~ 더 늦기전에 집에 가야지~\n얼른 들어가!"
        });

        //3. 아파트 옆 못난이 꽃 -두번째
        talkData.Add(3, new string[]
            { "ㅇㅇ뭐찾아?", "나 어디가야하는지 아는데.\n바로 옆 아파트 있지? 거기로 가면 돼",
                "(ㅎㅎ재밌당)","미안~\n이제 제대로 알려줄게 ㅎㅎ 위치", "ㅇㅇ"
            });

        //4. 오리 -네번째
        talkData.Add(4, new string[]{
            "흠, 날 찾는구나? 헤헤", "응! 난 고장난 것은 뭐든지 고칠 수 있어~","앗... 문이 고장났어? 고치려면 망치가 필요한데!",
            "걱정 마! 저~기 위치에 가면 공중에 떠있는 물체들이 있는데\n그걸 없애다보면, 망치가 나올거야!","큼큼,, 창피하지만" +
            "난 날지 못해서 그 물체들을 잡을 수 없어..=///=","와~ 찾아왔네?? 지금 고쳐줄게!"
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