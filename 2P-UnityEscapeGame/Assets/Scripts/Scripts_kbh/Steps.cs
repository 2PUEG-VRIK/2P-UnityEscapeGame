using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    Vector3 pos;//���� ���� ��ġ
    GameObject _obj;//���� ��
    public GameObject _objTrap;//������ Ʈ��
    Man player;//��~
    SpriteRenderer spr;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        player = GameObject.FindWithTag("Player").GetComponent<Man>();
        spr = player.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _obj = this.transform.gameObject;//_obj�� ���� ���� ��!
            pos = new Vector3(_obj.transform.position.x, _obj.transform.position.y, _obj.transform.position.z);
            StartCoroutine(Trap());


        }
      
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spr.material.color = Color.yellow;


        }

    }



    IEnumerator Trap()//�Ϲ� �� -> Ʈ�� �Ǿ�
    {
        yield return new WaitForSeconds(1f);

        Instantiate(_objTrap, pos, Quaternion.identity);//���� �ִ� ���� Ʈ�� ����
        _obj.transform.position=new Vector3(999,999,999);//��Ҵ� �� ���ְ�~

        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer�� �ٲ���
            StartCoroutine(decreaseHealth());



    }
    private int count = -1;
    IEnumerator decreaseHealth()//Ʈ������ �ִµ��� ü�� ���~ & ��� ���� ���������� ����
    {
        yield return new WaitForSeconds(3f);
        while (player.health > 0 && count==-1)
        {
            count++;
            player.health -= 1;//�Ӹ� ���� ü�� �����
            Debug.Log(player.health);
            spr.material.color = Color.red;
            StartCoroutine(decreaseHealth());
            
            
        }
    }
}
