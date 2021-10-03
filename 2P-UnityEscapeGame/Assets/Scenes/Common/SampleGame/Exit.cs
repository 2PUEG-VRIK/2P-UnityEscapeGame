using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{

    // 다음 스테이지..
    public string scene;

    public GameObject result;

    private int finPlayer = 0;

    GameObject[] players;

    bool isFinish;


    private void Start()
    {
        players = new GameObject[2];
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

    private void OnTriggerExit(Collider other)
    {
        if (!isFinish && other.gameObject.tag == "Player")
        {
            players[0] = null;
            finPlayer--;
        }
    }


    // 아래 함수 2개 바뀜.
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
            SingleGameMNG.Instance.Timer_Stop();

            StartCoroutine("StageFinish");
        }
    }
    IEnumerator StageFinish()
    {
        SingleGameMNG.Instance.save_time((scene == "Finish Scene"));

        yield return new WaitForSeconds(1f);
        players[0].GetComponent<Rigidbody>().useGravity = false;
        players[1].GetComponent<Rigidbody>().useGravity = false;

        isFinish = true;

        // 결과창 띄우기
        yield return new WaitForSeconds(2f);
        result.SetActive(true);
        SingleGameMNG.Instance.Game_Clear();
    }



}
