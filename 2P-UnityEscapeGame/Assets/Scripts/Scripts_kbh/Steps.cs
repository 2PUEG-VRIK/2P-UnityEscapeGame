using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    Vector3 pos;//���� ���� ��ġ
    GameObject _obj;//���� ��
    GameObject _objTrap;//������ Ʈ��
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _obj = this.transform.gameObject;//_obj�� ���� ���� ��!
            pos = new Vector3(_obj.transform.localPosition.x, _obj.transform.localPosition.y, _obj.transform.localPosition.z);
            _obj.SetActive(false);//��Ҵ� �� ���ְ�~
           
            

            
        }
      
    }

    

    IEnumerator Trap()//�Ϲ� �� -> Ʈ�� �Ǿ 1�ʸ��� ü�� ���̴� �� & ��� ���� ���������� ���ϱ�
    {
        Instantiate(_objTrap, pos, Quaternion.identity);//���� �ִ� ���� Ʈ�� ����
        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer�� �ٲ���

        yield return null;
    }
}
