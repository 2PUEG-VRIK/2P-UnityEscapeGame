using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{
    public GameObject Parent;//¿Ã¸®´Â ¾Öµé(¹¶ÅÖÀÌ)
    //Transform[] Children;//ºÎ¸ð ¼Ó °¢ °´Ã¼µé
    GameObject[] m;//ÃÊ·Ï ¸ó½ºÅÍ ¹è¿­

    GameObject[] Cubes; // Â¡°Ë´Ù¸®µé
    GameObject wall7;//¶³±¼ º®
    //GameObject[] allChildren;


    public int num;

    private void Awake()
    {
        wall7 = GameObject.Find("Wall (7)");
        //Children = Parent.gameObject.GetComponentsInChildren<Transform>();
        Parent = GameObject.FindWithTag("rise");

    }
    
    private void FixedUpdate()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");

        //for(int i=0;i<num;i++)

        if (m.Length == 3)
        {
            wall7.transform.Translate(Vector3.down, Space.Self);
            if (wall7.transform.position.y < -60)
                wall7.SetActive(false);
        }
        //c.transform.position += Vector3.up * 0.3f;
        //foreach(GameObject child in Cubes)
        //{
        //    child.transform.position += Vector3.up * 0.3f;
        //}
        //Parent.transform.position += Vector3.up * 0.3f;
        //Parent.transform.position

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.name == "Floor (1)")
        {
            Debug.Log("‰Ñ¶¥1");

            for (int i = 0; i < num-4; i++)
                GameObject.Find("Monster").transform.GetChild(i).gameObject.SetActive(true);
           
        }
    }
}
