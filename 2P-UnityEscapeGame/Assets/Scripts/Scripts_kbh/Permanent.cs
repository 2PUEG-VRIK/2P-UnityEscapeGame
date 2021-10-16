using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{

    //���Ϳ� ¡�˴ٸ� ����

    GameObject[] m;//�ʷ� ���� �迭
    GameObject wall7;//���� ��
    GameObject Step; //¡�˴ٸ� + �� ���� ������

    public int Total;
    private static int  Gnum;

    private void Awake()
    {
        wall7 = GameObject.Find("Wall (7)");
        Step = GameObject.Find("Steps");
    }
    
    private void FixedUpdate()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");
        Gnum = m.Length;
        if (m.Length == 4)//1�� �� �������� ����
        {
            wall7.transform.Translate(Vector3.down, Space.Self);
            if (wall7.transform.position.y < -60)
                wall7.SetActive(false);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.name == "Floor (1)")
        {
            Debug.Log("�Ѷ�1");

            for (int i = 0; i < Total-Gnum; i++)
                GameObject.Find("Monster").transform.GetChild(i).gameObject.SetActive(true);
           
        }
    }
}
