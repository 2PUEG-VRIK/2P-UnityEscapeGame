using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mngWhole1_3 : MonoBehaviour
{
    GameObject pickPos;//�Ӹ� ���� �ִ¾ֵ� ��ġ �÷��̾ �°� ����
    Vector3 playerPos;//�÷��̾�� ������~~

    // Start is called before the first frame update
    void Start()
    {
        pickPos = GameObject.Find("gababo");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = this.transform.position;

        pickPos.transform.position = new Vector3(playerPos.x, playerPos.y + 15f, playerPos.z);
        

    }
}
