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
    string DBurl = "https://escape-game-3382c-default-rtdb.firebaseio.com/";
    Text TotalTime;
    Text Rank;

    private void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);

        TotalTime = GameObject.Find("TotalTime").GetComponent<Text>();
        TotalTime.text = "총 소요시간 : " + SingleGameMNG.Instance.getSum() ;

        Rank = GameObject.Find("Rank").GetComponent<Text>();
        Rank.text =  "순위 : " + SingleGameMNG.Instance.getRank();
    }
}

 