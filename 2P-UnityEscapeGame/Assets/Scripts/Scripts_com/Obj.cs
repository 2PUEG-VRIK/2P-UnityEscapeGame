using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    public Transform manPos;


    private void OnTriggerStay(Collider other)
    {
        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        if (other.gameObject.tag == "Player")        // 플레이어가 초록버튼 누르면 가던 방향으로 x30
        {
            transform.position += manPos.forward * 25 * 0.75f * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        if (other.gameObject.tag == "Player")
        {
            rigid.velocity = manPos.forward * -0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
        if (other.gameObject.tag == "Player")
        {
            rigid.AddForce(manPos.forward * -15, ForceMode.VelocityChange);
        }
    }
}
