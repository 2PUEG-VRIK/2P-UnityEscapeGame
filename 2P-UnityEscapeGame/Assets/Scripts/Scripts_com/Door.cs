using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Updown은 버튼으로 위아래 열리는 문
    // Swing은 열쇠로 앞으로 열리는 문
    public enum CollectibleTypes { Updown, Swing }; // you can replace this with your own labels for the types of collectibles in your game!

    public CollectibleTypes DoorType; // this gameObject's type
    public AudioClip collectSound;


    public bool isOpen;

    private void Start()
    {
        isOpen = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Door - OnTriggerEnter");
        if (other.tag == "Player" && DoorType == CollectibleTypes.Swing)
        {
            Debug.Log("Door - Player");

            if (other.GetComponent<Man>().hasKey)
            {
                Debug.Log("Door - hasKey");

                isOpen = true;

                if (collectSound)
                    AudioSource.PlayClipAtPoint(collectSound, transform.position);

                //문 열리면 열쇠 없애고 비활성화
                GameObject key = GameObject.Find("Man").transform.GetChild(2).gameObject;
                other.GetComponent<Man>().hasKey = false;
                key.SetActive(false);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && DoorType == CollectibleTypes.Swing)
        {
            Debug.Log("Door - OnCollisionEnter");
        }
    }

    private void Update()
    {
        if (DoorType == CollectibleTypes.Swing)
        {
            if (isOpen)
            {
                 transform.GetChild(0).gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), Time.time * 0.2f);
            }
        }
    }
}
