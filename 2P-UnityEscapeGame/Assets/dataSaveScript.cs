using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class dataSaveScript : MonoBehaviour
{

    string fullpth = "Assets/Resources/kbh/mapData";
    StreamWriter sw;
    gameManager3 manager;


    // Start is called before the first frame update
    void Start()
    {
        if (false == File.Exists(fullpth))
            sw = new StreamWriter(fullpth + ".txt");

        manager = GameObject.Find("Man").GetComponent<gameManager3>();

    }

    void Update()
    {
        if (manager.saveData)//저장해야징
        {
            while (manager.que.Count > 0)
            {
                sw.WriteLine(manager.que.Dequeue() + " ", false);//false값은 append에 대한 값
            }

            sw.Flush(); sw.Close();
        }
    }
}
  
