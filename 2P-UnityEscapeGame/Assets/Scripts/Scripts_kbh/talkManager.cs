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
        //1.�� -ó��
        talkData.Add(1, new string[]
        {  "�� ������? ���� �������̾�?","��� ����? ����.\n���� ��� �����߿��� �δ����� ���� �ȶ��ϴܸ���! �� ����!"});

        //2.�δ�¡ -����°
        talkData.Add(2, new string[]
         {  "����! ���� �� �����", "�׺��� ��, ������ ������ ���� ��ġ�� ��������?","�� �ӿ����� �� ���\n����, ����������� �� ���ε����� �ɾ��",
         "�����̾�~ �� �ʱ����� ���� ������~\n�� ��!"
        });

        //3. ����Ʈ �� ������ �� -�ι�°
        talkData.Add(3, new string[]
            { "������ã��?", "�� ��𰡾��ϴ��� �ƴµ�.\n�ٷ� �� ����Ʈ ����? �ű�� ���� ��",
                "(������մ�)","�̾�~\n���� ����� �˷��ٰ� ���� ��ġ", "����"
            });

        //4. ���� -�׹�°
        talkData.Add(4, new string[]{
            "��, �� ã�±���? ����", "��! �� ���峭 ���� ������ ��ĥ �� �־�~","��... ���� ���峵��? ��ġ���� ��ġ�� �ʿ��ѵ�!",
            "���� ��! ��~�� ��ġ�� ���� ���߿� ���ִ� ��ü���� �ִµ�\n�װ� ���ִٺ���, ��ġ�� ���ðž�!","ŭŭ,, â��������" +
            "�� ���� ���ؼ� �� ��ü���� ���� �� ����..=///=","��~ ã�ƿԳ�?? ���� �����ٰ�!"
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