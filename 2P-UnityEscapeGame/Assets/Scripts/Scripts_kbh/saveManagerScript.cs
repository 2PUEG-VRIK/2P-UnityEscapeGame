using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class saveManagerScript : MonoBehaviour
{

    //string fullpth = "Assets/Resources/kbh/mapData";
    //StreamWriter sw;
    gameManager3 manager;
    //public bool twice = false;//맵 이동 한번했다가 원상태로 온 경우
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
        while (j.yes)
        {
            man.transform.position = (Vector3)j.q1.Dequeue();
            manager.check = (int)j.q1.Dequeue();
            manager.value = (int)j.q1.Dequeue();

        }

    }

    void Update()
    {

      
        //if (manager.saveData)//저장해야징
        //{
        //    while (manager.que.Count > 0)
        //    {
        //        sw.WriteLine(manager.que.Dequeue() + ",", false);//false값은 append에 대한 값
        //    }

        //    sw.Flush(); sw.Close();
        //}

        
    }
}
  
