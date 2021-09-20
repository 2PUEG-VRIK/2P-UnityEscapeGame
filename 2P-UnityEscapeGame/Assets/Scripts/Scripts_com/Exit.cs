using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public GameObject result;
    public GameObject GameManager;
    GameMNG GameMNG;

    private int finPlayer = 0;

    GameObject[] players;

    bool isFinish;
 

    private void Start()
    {
        players = new GameObject[2];
        GameMNG = GameManager.GetComponent<GameMNG>();
        isFinish = false;
    }

    private void Update()
    {
        FinishMove();
    }

    public void FinishMove()
    {
        if (isFinish)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * 15);
            players[0].transform.position = Vector3.MoveTowards(players[0].transform.position, players[0].transform.position + Vector3.up, Time.deltaTime * 15);
            players[1].transform.position = Vector3.MoveTowards(players[1].transform.position, players[1].transform.position + Vector3.up, Time.deltaTime * 15);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFinish && other.gameObject.tag == "Player")
        {
            players[finPlayer] = other.gameObject;

            finPlayer++;
        }
        if (finPlayer == 2)
        {
            players[1] = other.gameObject;
            GameMNG.Timer_Stop();

            StartCoroutine("StageFinish");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isFinish && other.gameObject.tag == "Player")
        {
            players[0] = null;
            finPlayer--;
        }
    }


    IEnumerator StageFinish()
    {
        yield return new WaitForSeconds(1f);
        players[0].GetComponent<Rigidbody>().useGravity = false;
        players[1].GetComponent<Rigidbody>().useGravity = false;
        
        
        isFinish = true;

        // 결과창 띄우기
        yield return new WaitForSeconds(2f);
        result.SetActive(true);
        GameMNG.Game_Clear(); 
    }
 

  
}
