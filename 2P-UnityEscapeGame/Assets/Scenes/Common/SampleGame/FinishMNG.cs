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
        TotalTime.text = "총 소요시간 : " + SingleGameMNG.Instance.getSum() ;

        // 내 순위 
        myRank = SingleGameMNG.Instance.getRank();

        int i = 1;
        // 1~5위 출력
        foreach (var item in SingleGameMNG.Instance.getTop5())
        {
            // 각 Text에 출력하기.
            // i 번째니까 RankingList_i라는 항목을 찾아야함.
  
            GameObject obj1 = GameObject.Find("RankingList_" + i.ToString()).gameObject; // Object의 이름을 찾음. 가장 처음에 나오는 Object를 반환.
            GameObject obj2 = obj1.transform.GetChild(1).gameObject; // 자식을 번호로 찾음. 
            GameObject obj3 = obj1.transform.GetChild(2).gameObject; // 자식을 번호로 찾음. 
            GameObject obj4 = obj1.transform.GetChild(0).gameObject; // 자식을 번호로 찾음. 

            obj2.GetComponent<Text>().text = item.Key;
            obj3.GetComponent<Text>().text = item.Value.ToString();

            if (i == myRank && i < 6)
            {
                // 순위권일때
                obj2.GetComponent<Text>().color = new Color(1f, 0.72851f, 0f);
                obj3.GetComponent<Text>().color = new Color(1f, 0.72851f, 0f);
                obj4.GetComponent<Text>().color = new Color(1f, 0.72851f, 0f);


                GameObject obj5 = GameObject.Find("RankingList_my").gameObject; // Object의 이름을 찾음. 가장 처음에 나오는 Object를 반환.
                GameObject obj6 = obj5.transform.GetChild(1).gameObject; // 자식을 번호로 찾음.
                obj6.GetComponent<Text>().text = i + "등 입니다!";
            }
            i++;
        }

        if(myRank > 5)
        {
            GameObject obj1 = GameObject.Find("RankingList_my").gameObject; // Object의 이름을 찾음. 가장 처음에 나오는 Object를 반환.
            GameObject obj2 = obj1.transform.GetChild(1).gameObject; // 자식을 번호로 찾음. 
            GameObject obj3 = obj1.transform.GetChild(2).gameObject; // 자식을 번호로 찾음. 
            GameObject obj4 = obj1.transform.GetChild(0).gameObject; // 자식을 번호로 찾음. 

            obj4.GetComponent<Text>().text = SingleGameMNG.Instance.getRank().ToString();
            obj2.GetComponent<Text>().text = SingleGameMNG.Instance.playername;
            obj3.GetComponent<Text>().text = SingleGameMNG.Instance.getSum();
        }
        
    }

     
}

 