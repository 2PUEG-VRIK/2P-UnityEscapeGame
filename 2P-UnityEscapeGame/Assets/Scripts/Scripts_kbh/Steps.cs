using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    Vector3 pos;//밟은 땅의 위치
    GameObject _obj;//밟은 땅
    GameObject _objTrap;//생성할 트랩
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _obj = this.transform.gameObject;//_obj는 내가 밟은 땅!
            pos = new Vector3(_obj.transform.localPosition.x, _obj.transform.localPosition.y, _obj.transform.localPosition.z);
            _obj.SetActive(false);//밟았던 곳 없애고~
           
            

            
        }
      
    }

    

    IEnumerator Trap()//일반 블럭 -> 트랩 되어서 1초마다 체력 깎이는 것 & 사람 색깔 빨간색으로 변하기
    {
        Instantiate(_objTrap, pos, Quaternion.identity);//내가 있던 곳에 트랩 생성
        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer로 바꿔줌

        yield return null;
    }
}
