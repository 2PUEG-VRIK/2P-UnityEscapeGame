using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen; // 위아래로 열리는것
    public bool isSlide; // 끼이익 열리는것

    private void Start()
    {
        isOpen = false;
        isSlide = false;
    }
     

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("플레이어 닿음.");
            if (other.GetComponent<Man>().hasKey)
            {
                isSlide = true;
            }

        }
    }

    private void Update()
    {
        if(isSlide)
        {
            Debug.Log("isSlide");
            //transform.position += Vector3.up;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.time * 0.01f);
        }
    }
}
