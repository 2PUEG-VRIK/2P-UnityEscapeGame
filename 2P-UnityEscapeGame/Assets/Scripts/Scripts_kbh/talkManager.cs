using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class talkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public int length;
    public int value;//npc�� ������ȣ
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
        //1.�� (���� ó�� ������ npc)
        talkData.Add(1, new string[]
        {  "�� ������? ���� �������̾�?","��� ����? ����.\n��ġ�� ������ ������ ���� ~."});
        
        //2.�δ�¡
        talkData.Add(2, new string[]
         {  "���� ���� �� �����", "���� �ȹϴ´ٴ�"
        });

        //3. ����Ʈ �� ������ ��
        talkData.Add(3, new string[]
            { "��(2)", "��(4)", "(������մ�)"
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