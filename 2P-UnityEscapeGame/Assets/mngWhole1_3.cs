using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mngWhole1_3 : MonoBehaviour
{
    GameObject pickPos;//머리 위에 있는애들 위치 플레이어에 맞게 고정
    Vector3 playerPos;//플레이어읭 포지션~~

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
