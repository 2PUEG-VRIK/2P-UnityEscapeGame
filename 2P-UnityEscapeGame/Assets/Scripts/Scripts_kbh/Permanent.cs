using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{

    //���Ϳ� ¡�˴ٸ� ����
    GameObject[] m;//�ʷ� ���� �迭
    GameObject[] Walls;//ȸ���� ����
    private int wallNum;//grave wall ����
    GameObject Step; //¡�˴ٸ� + �� ���� ������

    public int Total;
    private static int  Gnum;

    private void Awake()
    {
        Step = GameObject.Find("Steps");
        Walls = GameObject.FindGameObjectsWithTag("Things");
        wallNum = 0;
    }

    private void Update()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");
        Gnum = m.Length;
        if (Gnum == 0)
        {
            foreach (GameObject wall in Walls)
            {
                wall.transform.rotation = Quaternion.Slerp(
                wall.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.time * 0.005f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Plain")
        {
            for (int i = 0; i < Total-Gnum; i++)
                GameObject.Find("Monster").transform.GetChild(i).gameObject.SetActive(true);
<<<<<<< Updated upstream
=======
            time = 0.0f; isTimerOn = true; check_p = 0;
            first = true;
        }
    }
    IEnumerator popHowTo()
    {
        if (2f < time && time < 7f)
        {
            howTo.SetActive(true);
            if (check_p == 0)
                StartCoroutine(popUpAudioCo());

        }
        else if (time > 7f)
        {
            howTo.SetActive(false);
            isTimerOn = false;
            StopCoroutine(popHowTo());
        }
        yield return null;
    }

    IEnumerator popHowTo2()
    {
        if (2f < time && time < 7f)
        {
            howTo.SetActive(true);
            howTo.transform.GetChild(0).gameObject.SetActive(false);
            howTo.transform.GetChild(1).gameObject.SetActive(true);
            //howTo.GetComponent<Text>().text = 
            if (check_p == 0)
                StartCoroutine(popUpAudioCo());

>>>>>>> Stashed changes
        }
    }
}
