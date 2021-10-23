using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void Update()
    {

    }

    void GenerateData()
    {
        //index, string
        //1.�� ���� ó�� ������ npc
        talkData.Add(1, new string[]
        { "..", "�� ������? ���� ��¾���̾�?","��� ������? �ƴ�, ���þ�. ����� ��鸸 ������ ���̾�."});



    }

    public string GetTalk(int id, int talkIndex)
    {
        return talkData[id][talkIndex];
    }
}