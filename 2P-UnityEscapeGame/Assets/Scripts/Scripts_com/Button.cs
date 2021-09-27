using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;
    ParticleSystem particle;
    Material mat;

    bool isPushed = false;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.gameObject.SetActive(false);
        mat = GetComponent<MeshRenderer>().material;
    }
    private void OnTriggerStay(Collider other)
    {
        // 꼭 플레이어가 누를 필요없음. 그냥 뭐가 누르고 있으면 문 열어

        // 닿는 순간 버튼 눌린 액션
        if (!isPushed)
        {
            transform.position += new Vector3(0, -0.99f, 0);
            isPushed = true;
            mat.color = Color.yellow;

            particle.gameObject.SetActive(true);
        }

        if (isPushed)
        {
            if (!door.GetComponent<Door>().isOpen)
            {
                //Rigidbody doorRigid = door.GetComponent<Rigidbody>();
                //doorRigid.AddForce(new Vector3(0, 500.0f, 0));
                door.transform.position += Vector3.up * 15;
                door.GetComponent<Door>().isOpen = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPushed)
        {
            transform.position += new Vector3(0, +0.99f, 0);
            isPushed = false;
            mat.color = Color.white;

            if (door.GetComponent<Door>().isOpen)
            {
                door.transform.position += Vector3.up * -15;
                door.GetComponent<Door>().isOpen = false;
            }
            particle.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }
}
