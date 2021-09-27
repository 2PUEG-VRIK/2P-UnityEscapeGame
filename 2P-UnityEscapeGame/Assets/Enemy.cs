using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth;
    public int curHealth;
    Material mat;

    Rigidbody rigid;
    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            Debug.Log("��ġ�� ������ ���� ü���� " + curHealth);
            StartCoroutine(OnDamage());
        }

        else if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Debug.Log("�������� ������ ���� ü���� " + curHealth);
            StartCoroutine(OnDamage());

        }
    }

    IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
            mat.color = Color.white;

        else
        {
            mat.color = Color.grey;
            gameObject.layer = 14;//EnemyDead�� �ٲ�
            Destroy(gameObject, 4);
        }
    }
}
