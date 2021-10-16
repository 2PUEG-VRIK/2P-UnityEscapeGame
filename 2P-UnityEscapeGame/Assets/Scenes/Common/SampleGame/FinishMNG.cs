using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FinishMNG : MonoBehaviour
{
    Text TotalTime;
    int myRank;

    private void Start()
    {
        TotalTime = GameObject.Find("TotalTime").GetComponent<Text>();
        TotalTime.text = "�� �ҿ�ð� : " + SingleGameMNG.Instance.getSum() ;

        // �� ���� 
        myRank = SingleGameMNG.Instance.getRank();

        int i = 1;
        // 1~5�� ���
        foreach (var item in SingleGameMNG.Instance.getTop5())
        {
            // �� Text�� ����ϱ�.
            // i ��°�ϱ� RankingList_i��� �׸��� ã�ƾ���.
  
            GameObject obj1 = GameObject.Find("RankingList_" + i.ToString()).gameObject; // Object�� �̸��� ã��. ���� ó���� ������ Object�� ��ȯ.
            GameObject obj2 = obj1.transform.GetChild(1).gameObject; // �ڽ��� ��ȣ�� ã��. 
            GameObject obj3 = obj1.transform.GetChild(2).gameObject; // �ڽ��� ��ȣ�� ã��. 
            GameObject obj4 = obj1.transform.GetChild(0).gameObject; // �ڽ��� ��ȣ�� ã��. 

            obj2.GetComponent<Text>().text = item.Key;
            obj3.GetComponent<Text>().text = item.Value.ToString();

            if (i == myRank && i < 6)
            {
                // �������϶�
                obj2.GetComponent<Text>().color = new Color(1f, 0.72851f, 0f);
                obj3.GetComponent<Text>().color = new Color(1f, 0.72851f, 0f);
                obj4.GetComponent<Text>().color = new Color(1f, 0.72851f, 0f);


                GameObject obj5 = GameObject.Find("RankingList_my").gameObject; // Object�� �̸��� ã��. ���� ó���� ������ Object�� ��ȯ.
                GameObject obj6 = obj5.transform.GetChild(1).gameObject; // �ڽ��� ��ȣ�� ã��.
                obj6.GetComponent<Text>().text = i + "�� �Դϴ�!";
            }
            i++;
        }

        if(myRank > 5)
        {
            GameObject obj1 = GameObject.Find("RankingList_my").gameObject; // Object�� �̸��� ã��. ���� ó���� ������ Object�� ��ȯ.
            GameObject obj2 = obj1.transform.GetChild(1).gameObject; // �ڽ��� ��ȣ�� ã��. 
            GameObject obj3 = obj1.transform.GetChild(2).gameObject; // �ڽ��� ��ȣ�� ã��. 
            GameObject obj4 = obj1.transform.GetChild(0).gameObject; // �ڽ��� ��ȣ�� ã��. 

            obj4.GetComponent<Text>().text = SingleGameMNG.Instance.getRank().ToString();
            obj2.GetComponent<Text>().text = SingleGameMNG.Instance.playername;
            obj3.GetComponent<Text>().text = SingleGameMNG.Instance.getSum();
        }
        
    }

     
}

 