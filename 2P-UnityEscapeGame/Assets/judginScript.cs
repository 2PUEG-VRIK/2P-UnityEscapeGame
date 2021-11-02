using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judginScript : MonoBehaviour
{
    //갔다왔는지 아닌지 판별하는 script
    // Start is called before the first frame update
    public ArrayList arr1 = new ArrayList();
    saveManagerScript manager;
    gameManager3 gameManager;
    public bool yes = false;
    public bool yes_2 = false;
    void Start()
    {
        manager = GameObject.FindWithTag("Player").GetComponent<saveManagerScript>();
        gameManager = GameObject.FindWithTag("Player").GetComponent<gameManager3>();

    }

    public void saveQue(int x, int y, int z, int c, int v, int myIn, int yourIn)//check와 value 받는댜
    {
        if (gameManager.check == -1)
            gameManager.check = 2;
        else if (gameManager.check == 4)
        {
            gameManager.check = -4;
            myIn = 6;
            yourIn = 5;
        }
        Debug.Log(gameManager.check);
        arr1.Add(x);
        arr1.Add(y);
        arr1.Add(z);
        arr1.Add(gameManager.check);
        arr1.Add(v);
        arr1.Add(myIn);//꽃이랑 대화이어가야하므로 내 index
        arr1.Add(yourIn);//꽃 index 저장

        for (int i = 0; i < arr1.Count; i++)
            Debug.Log(arr1[i]);
    }
}