using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDoor : MonoBehaviour
{
    public int mh; // 문 원래 체력
    public int ch; // 문 현재 체력
    public int value;
    Material mat;
    Material pre;//원래 거 저장하는 변수

    Rigidbody rigid;
    BoxCollider boxcollider;
    BoxCollider PBoxcollider;
    private int doorRotCheck = -1;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        PBoxcollider= GetComponent<BoxCollider>();

        mat = GetComponentInChildren<MeshRenderer>().material;
        pre = GetComponentInChildren<MeshRenderer>().material;
        StartCoroutine(OnDamage());
    }

    private void Update()
    {
        if(doorRotCheck==1)
            StartCoroutine(doorRotation());
    }
    private int now;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee" && value==0)
        {
            Weapon weapon = other.GetComponent<Weapon>();
            ch -= weapon.damage;
            Debug.Log("망치로 때림ㅜ 현재 체력은 " + ch);
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage());
        }

        else if (other.tag == "Bullet" )
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.damage == 10 && value == 1)
            {
                Debug.Log("총이다");
                ch -= bullet.damage;
                StartCoroutine(OnDamage());
            }

            else if (bullet.damage != 10 && value == 2)
            {
                Debug.Log("총이다");
                ch -= bullet.damage;
                StartCoroutine(OnDamage());
            }
            now = value;
            boxcollider = this.gameObject.GetComponent<BoxCollider>();
            PBoxcollider= this.gameObject.transform.parent.GetComponent<BoxCollider>();
                Destroy(other.gameObject);//적에 닿는순간 총알 안보이게 하기~ 관통하면 안되니까
        }
    }
    IEnumerator OnDamage()
    {
        //mat.color = Color.grey;
        yield return new WaitForSeconds(1f);
        mat.color = pre.color;

        if(ch<=0)
        {   //문 체력 다 닳음
            doorRotCheck = 1;
        }
    }

    IEnumerator doorRotation()
    {
        if (now==2)//파란문
            this.transform.rotation = Quaternion.Slerp(
               this.transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), Time.time * 0.001f);

        else // 노란, 연두색 문
            this.transform.rotation = Quaternion.Slerp(
               this.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.time * 0.001f);

        PBoxcollider.enabled = false;
        boxcollider.enabled = false;
    yield return null;
    }
}


