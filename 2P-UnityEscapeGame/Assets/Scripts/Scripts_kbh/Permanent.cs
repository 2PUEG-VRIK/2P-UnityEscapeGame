using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{
    public GameObject Parent;//�ø��� �ֵ�(������)
    //Transform[] Children;//�θ� �� �� ��ü��
    GameObject[] m;//�ʷ� ���� �迭
    GameObject[] m2;//����� ����

    GameObject[] Cubes; // ¡�˴ٸ���
    GameObject wall7;//���� ��
    //GameObject[] allChildren;

    GameObject player;
    GameObject Floor; //�÷��̾ �����ϸ� ���� ����� ��(������ ��)
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
    //        Debug.Log("�Ѷ�1");

    //    if (collision.transform.name == "floor (1)")
    //    {
    //        for (int i = 0; i < 5; i++)
    //            GameObject.Find("MONSTER").transform.GetChild(i).gameObject.SetActive(true);
    //    }


        //}
        //private void LateUpdate()
        //{
        //    m2 = GameObject.FindGameObjectsWithTag("Enemy");//����� �ֵ� �� �ľ�
        //}


    
}
