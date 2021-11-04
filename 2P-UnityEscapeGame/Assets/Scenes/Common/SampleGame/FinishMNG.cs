using Firebase;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class FinishMNG : MonoBehaviour
{ 

    private void Start()
    {
       
        
    }

    public void ExitGame()
    {
        SingleGameMNG.Instance.Timer_Stop();

    }

    public void RestartGame()
    {

        SceneManager.LoadScene("Stage01");
    }

}

 