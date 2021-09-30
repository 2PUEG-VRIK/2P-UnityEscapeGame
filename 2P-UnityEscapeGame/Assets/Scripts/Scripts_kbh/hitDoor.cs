using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDoor : MonoBehaviour
{
    public int mh; // 문 원래 체력
    public int ch; // 문 현재 체력
    public int value;
    Material mat;

    Rigidbody rigid;
    BoxCollider boxcollider;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee" &&value==0)
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

                Destroy(other.gameObject);//적에 닿는순간 총알 안보이게 하기~ 관통하면 안되니까
        }
    }

    IEnumerator OnDamage()
    {
        mat.color = Color.grey;
        yield return new WaitForSeconds(0.1f);

        if (ch > 0)
            mat.color = Color.white;

        else
        {//문 체력 다 닳음
            mat.color = Color.white;
           // gameObject.layer = 14;//EnemyDead로 바꿔
           // rigid.AddForce(, ForceMode.Impulse);
            Destroy(gameObject, 0.5f); //1초 뒤에 사라짐

            //사라진 그 자리에 아이템 하나 넣어주기
        }
    }
}


