using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{
    public GameObject Parent;//올리는 애들(뭉텅이)
    //Transform[] Children;//부모 속 각 객체들
    GameObject[] m;//초록 몬스터 배열
    GameObject[] m2;//보라색 몬스터

    GameObject[] Cubes; // 징검다리들
    GameObject wall7;//떨굴 벽
    //GameObject[] allChildren;

    GameObject player;
    GameObject Floor; //플레이어가 도착하면 몬스터 생기는 땅(오른쪽 거)
    Enemy monster;

    private void Awake()
    {
        wall7 = GameObject.Find("Wall (7)");
        //Children = Parent.gameObject.GetComponentsInChildren<Transform>();
        Parent = GameObject.FindWithTag("rise");
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");


    }
    private void Update()
    {
        if (m.Length == 0)
        {
            wall7.transform.Translate(Vector3.down, Space.Self);
            if (wall7.transform.position.y < -60)
                wall7.SetActive(false);
        }
        //c.transform.position += Vector3.up * 0.3f;
        //foreach(GameObject child in Cubes)
        //{
        //    child.transform.position += Vector3.up * 0.3f;
        //}
        //Parent.transform.position += Vector3.up * 0.3f;
        //Parent.transform.position

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //        Debug.Log("됏땅1");

    //    if (collision.transform.name == "floor (1)")
    //    {
    //        for (int i = 0; i < 5; i++)
    //            GameObject.Find("MONSTER").transform.GetChild(i).gameObject.SetActive(true);
    //    }


        //}
        //private void LateUpdate()
        //{
        //    m2 = GameObject.FindGameObjectsWithTag("Enemy");//보라색 애들 수 파악
        //}


    
}
