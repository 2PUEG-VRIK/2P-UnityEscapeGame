using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{
 //   public int number;//몬스터 수
    public GameObject[] m;//몬스터 배열
    public Transform Rise;//부모(뭉텅이)
    public Transform[] Children;//부모 속 각 객체들
    
    GameObject wall7;
    Transform target1;//떨굴 벽 위치정보
    //GameObject[] allChildren;


    private void Start()
    {
        wall7 = GameObject.Find("Wall (7)");
        target1 = wall7.GetComponent<Transform>();
        Children = Rise.gameObject.GetComponentsInChildren<Transform>();
        
    }
    private void FixedUpdate()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");
        //      number = m.Length;
        if (m.Length == 0)
        {

            wall7.transform.Translate(Vector3.down, Space.Self);

            while (Rise.position.y != 3)
                Rise.Translate(Vector3.up, Space.Self);

            //foreach(Transform child in Rise)
            // {
            //     if(child.name==transform.name)
            //         child.position = new Vector3(102.39f, 3, -23.150f);

            // }

            if (target1.position.y == -90)
                Destroy(wall7);


        }

    }

}
