using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updown : MonoBehaviour
{
    public float rightMax = 2.0f; //좌로 이동가능한 (x)최대값
    public float leftMax = -2.0f; //우로 이동가능한 (x)최대값
    float posx; //현재 위치(x) 저장
    float posy; //현재 위치(y) 저장
    float posz; //현재 위치(z) 저장
    public float speed = 3.0f; // 속도
    float direction = 1.0f; //방향


    void Start()
    {
        posx = transform.position.x;
        posy = transform.position.y;
        posz = transform.position.z;
    }

    void Update()
    {
        if (!(this.GetComponent<Activator>()?.isOn ?? true))
        {
            return;
        }
        posx += Time.deltaTime * direction * speed;

        if (posx >= rightMax)
        {
            direction *= -1;
            posx = rightMax;
        }

        //현재 위치(x)가 우로 이동가능한 (x)최대값보다 크거나 같다면
        //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 우로 이동가능한 (x)최대값으로 설정
        else if (posx <= leftMax)
        {

            direction *= -1;
            posx = leftMax;
        }

        Rigidbody rigid = this.GetComponent<Rigidbody>();
        //현재 위치(x)가 좌로 이동가능한 (x)최대값보다 크거나 같다면
        //이동속도+방향에 -1을 곱해 반전을 해주고 현재위치를 좌로 이동가능한 (x)최대값으로 설정
        //rigid.MovePosition(new Vector3(posx, posy, posz));
        transform.position = new Vector3(posx, posy, posz);
        //"Stone"의 위치를 계산된 현재위치로 처리
    }
}
