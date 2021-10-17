using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{

    //���Ϳ� ¡�˴ٸ� ����

    GameObject[] m;//�ʷ� ���� �迭
    GameObject[] Walls;//ȸ���� ����
    private int wallNum;//grave wall ����
    GameObject Step; //¡�˴ٸ� + �� ���� ������

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
        foreach(GameObject wall in Walls)
        {
            wall.transform.rotation = Quaternion.Slerp(
            wall.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.time * 0.01f);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.name == "Plain")
        {
            Debug.Log("�Ѷ�1");

            for (int i = 0; i < Total-Gnum; i++)
                GameObject.Find("Monster").transform.GetChild(i).gameObject.SetActive(true);
           
        }
    }
}
