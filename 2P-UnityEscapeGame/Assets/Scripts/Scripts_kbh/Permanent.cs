using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{

    //몬스터와 징검다리 관할

   // public GameObject Parent;//올리는 애들(뭉텅이)
    //Transform[] Children;//부모 속 각 객체들
    GameObject[] m;//초록 몬스터 배열

    //GameObject[] Cubes; // 징검다리들
    GameObject wall7;//떨굴 벽
    GameObject Step; //징검다리 + 그 위의 아이템

    public int Total;
    private static int  Gnum;

    private void Awake()
    {
        wall7 = GameObject.Find("Wall (7)");
        //Children = Parent.gameObject.GetComponentsInChildren<Transform>();
        //Parent = GameObject.FindWithTag("rise");
        Step = GameObject.Find("Steps");

    }
    
    private void FixedUpdate()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");
        Gnum = m.Length;
        if (m.Length == 0)
        {
            wall7.transform.Translate(Vector3.down, Space.Self);
            if (wall7.transform.position.y < -60)
                wall7.SetActive(false);

            //for (int i = 0; i < Step.transform.childCount; i++)
            //    GameObject.Find("Steps").transform.GetChild(i).gameObject.SetActive(true);

        }


        //c.transform.position += Vector3.up * 0.3f;
        //foreach(GameObject child in Cubes)
        //{
        //    child.transform.position += Vector3.up * 0.3f;
        //}
        //Parent.transform.position += Vector3.up * 0.3f;
        //Parent.transform.position

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.name == "Floor (1)")
        {
            Debug.Log("됏땅1");

            for (int i = 0; i < Total-Gnum; i++)
                GameObject.Find("Monster").transform.GetChild(i).gameObject.SetActive(true);
           
        }
    }
}
