using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDoor : MonoBehaviour
{
    public int mh; // �� ���� ü��
    public int ch; // �� ���� ü��
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
            Debug.Log("��ġ�� ������ ���� ü���� " + ch);
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage());

        }

        else if (other.tag == "Bullet" )
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet.damage == 10 && value == 1)
            {
                Debug.Log("���̴�");
                ch -= bullet.damage;
                StartCoroutine(OnDamage());
            }

            else if (bullet.damage != 10 && value == 2)
            {
                Debug.Log("���̴�");
                ch -= bullet.damage;
                StartCoroutine(OnDamage());
            }

                Destroy(other.gameObject);//���� ��¼��� �Ѿ� �Ⱥ��̰� �ϱ�~ �����ϸ� �ȵǴϱ�
        }
    }

    IEnumerator OnDamage()
    {
        mat.color = Color.grey;
        yield return new WaitForSeconds(0.1f);

        if (ch > 0)
            mat.color = Color.white;

        else
        {//�� ü�� �� ����
            mat.color = Color.white;
           // gameObject.layer = 14;//EnemyDead�� �ٲ�
           // rigid.AddForce(, ForceMode.Impulse);
            Destroy(gameObject, 0.5f); //1�� �ڿ� �����

            //����� �� �ڸ��� ������ �ϳ� �־��ֱ�
        }
    }
}


