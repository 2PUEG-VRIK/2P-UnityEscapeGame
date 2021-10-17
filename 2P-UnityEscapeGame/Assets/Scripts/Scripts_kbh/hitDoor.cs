using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitDoor : MonoBehaviour
{
    public int mh; // �� ���� ü��
    public int ch; // �� ���� ü��
    public int value;
    Material mat;
    Material pre;//���� �� �����ϴ� ����

    Rigidbody rigid;
    BoxCollider boxcollider;
    private int doorRotCheck = -1;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;
        pre = GetComponentInChildren<MeshRenderer>().material;
        StartCoroutine(OnDamage());
    }

    private void Update()
    {
        if(doorRotCheck==1)
            StartCoroutine(doorRotation());
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
        yield return new WaitForSeconds(1f);
        mat.color = pre.color;
        Debug.Log("���� �ȹٱ;����");

        if(ch==0)
        {   //�� ü�� �� ����
            doorRotCheck = 1;
           
        }
    }

    IEnumerator doorRotation()
    {
        if (this.value == 1)//�Ķ���
        {
            this.transform.rotation = Quaternion.Slerp(
               this.transform.rotation, Quaternion.Euler(new Vector3(0, 180, 0)), Time.time * 0.01f);
        }

        else // ���, ���λ� ��
            this.transform.rotation = Quaternion.Slerp(
               this.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.time * 0.01f);
    
    yield return null;
    }
}


