using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judginScript : MonoBehaviour
{
    //���ٿԴ��� �ƴ��� �Ǻ��ϴ� script
    // Start is called before the first frame update
    public ArrayList arr1 = new ArrayList();
    saveManagerScript manager;
    gameManager3 gameManager;
    public bool yes = false;
    void Start()
    {
        manager = GameObject.FindWithTag("Player").GetComponent<saveManagerScript>();
        gameManager = GameObject.FindWithTag("Player").GetComponent<gameManager3>();

    }

    // Update is called once per frame
    void Update()
    {
        if (yes)
        {

        }
    }

    public void saveQue(int x, int y, int z, int c, int v, int myIn, int yourIn)//check�� value �޴´�
    {
        arr1.Add(x);
        arr1.Add(y);
        arr1.Add(z);
        arr1.Add(2);
        arr1.Add(v);
        arr1.Add(myIn);//���̶� ��ȭ�̾���ϹǷ� �� index
        arr1.Add(yourIn);//�� index ����
        //arr1[0] = x;
        //arr1[1] = y;
        //arr1[2] = z;
        //arr1[3] = 2;
        //arr1[4] = v;

        gameManager.check = 2;
    }
}