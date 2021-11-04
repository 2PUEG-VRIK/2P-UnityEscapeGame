using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{

    //몬스터와 징검다리 관할
    GameObject[] m;//초록 몬스터 배열
    GameObject[] Walls;//회전할 벽들
    private int wallNum;//grave wall 개수
    GameObject Step; //징검다리 + 그 위의 아이템

    public int Total;
    private static int  Gnum;

    private void Awake()
    {
        Step = GameObject.Find("Steps");
        Walls = GameObject.FindGameObjectsWithTag("Things");
        wallNum = 0;
    }

    private void Update()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");
        Gnum = m.Length;
        if (Gnum == 0)
        {
            foreach (GameObject wall in Walls)
            {
                wall.transform.rotation = Quaternion.Slerp(
                wall.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.time * 0.005f);
            }
        }
    }

    private bool first = false;
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.name == "Plain" && !first)
        //{
            
        //    for (int i = 0; i < 5; i++)
        //        GameObject.Find("Monster").transform.GetChild(i).gameObject.SetActive(true);
        //    first = true;
        //}


    }
}
