using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanent : MonoBehaviour
{
    public GameObject Parent;//�ø��� �ֵ�(������)
    //Transform[] Children;//�θ� �� �� ��ü��
    
    GameObject[] m;//���� �迭

    GameObject wall7;//���� ��
    //GameObject[] allChildren;

    int check = -1;

    
    private void Awake()
    {
        wall7 = GameObject.Find("Wall (7)");
        //Children = Parent.gameObject.GetComponentsInChildren<Transform>();
       // Parent = GameObject.FindWithTag("rise");
       

    }
    private void FixedUpdate()
    {
        m = GameObject.FindGameObjectsWithTag("Enemy");
        
    }
    private void Update()
    {
        if(check==-1) Co();
        // if (m.Length ==0 && check==-1) RiseUp();


        if (wall7.transform.position.y <= -60)//|| Parent.transform.position.y > -2
        {
            //wall7.transform.position = new Vector3(52.8f, -50, 0);
            wall7.SetActive(false);
            StopCoroutine("Falldown");

        }

        //if (Parent.transform.position.y >= -2)
        //{
        //    check--;
        //    Debug.Log("������ ������~~~~~~~`");


        //    StopCoroutine("RiseUpFunc");

        //}

    }
    void Co()//�ڷ�ƾ ���� �Լ�
    {
        StartCoroutine("Falldown");
       // StartCoroutine("RiseUpFunc");
        
         

    }
    IEnumerator Falldown()//�� ������ �Լ�
    {
        wall7.transform.Translate(Vector3.down, Space.Self);
        yield return null;
    }
   
    //IEnumerator RiseUpFunc()
    //{

    //    //Parent.transform.Translate(Vector3.up.normalized);

    //    //foreach (Transform child in Parent)
    //    //{
    //    //    rigid.AddForce(new Vector3(0, -1, 0));
    //    //    //child.transform.Translate(new Vector3(0,0.5f,0));
    //    //    if (child.transform.position.y > 0)
    //    //     

    //    //}
    //    Parent.transform.position += Vector3.up * 0.3f;

       
    //    yield return StartCoroutine(Falldown());
    //}
        

       
}
