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
        CheckLength(value);
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
        {  "넌 누구야? 여긴 어쩐일이야?","노란 강아지? \n아니, 못봤어. 여기는 양들만 지내는 곳이야."});



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