using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class saveManagerScript : MonoBehaviour
{

    //string fullpth = "Assets/Resources/kbh/mapData";
    //StreamWriter sw;
    gameManager3 manager;
    //public bool twice = false;//�� �̵� �ѹ��ߴٰ� �����·� �� ���
    judginScript j;
    GameObject man;

    // Start is called before the first frame update
    void Start()
    {
        //if (false == File.Exists(fullpth))
        //    sw = new StreamWriter(fullpth + ".txt");

        manager = GameObject.Find("Man").GetComponent<gameManager3>();
        man = GameObject.FindWithTag("Player");
        j = GameObject.Find("judging").GetComponent<judginScript>();
        if (j.yes)
        {
            man.transform.position = (Vector3)j.q1.Dequeue();
            manager.check = (int)j.q1.Dequeue();
            manager.value = (int)j.q1.Dequeue();

        }

    }

    void Update()
    {

      
        //if (manager.saveData)//�����ؾ�¡
        //{
        //    while (manager.que.Count > 0)
        //    {
        //        sw.WriteLine(manager.que.Dequeue() + ",", false);//false���� append�� ���� ��
        //    }

        //    sw.Flush(); sw.Close();
        //}

        
    }
}
  
