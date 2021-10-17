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
    private float time = 0f;//¡�˴ٸ��� �ִ� �ð� ���(2��)
    bool isOn = false;//¡�˴ٸ��� �ö���ֵ�
    bool timerOn = false;
    GameObject overHead;//�Ӹ����� ü�� -1 �ߴ°�
    Vector3 overHeadPos;
    Transform playerPos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();
        player = GameObject.FindWithTag("Player").GetComponent<Man>();
        spr = player.GetComponent<SpriteRenderer>();
        overHead = GameObject.Find("overHeadCount");

    }

    private void Update()
    {

        if (isOn)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //collider Exit ����
            {
                time = 0f;
                Debug.Log("�̰� �Ƴ�? �����̽��ٴ����¶�");
                timerOn = false;
                isOn = false;
                CancelInvoke("decreaseHealth");
            }
            else if (timerOn)
                Timer();
        }


        if (player.health == 0)
            Debug.Log("������");

        playerPos = player.transform;
       

    }

    private void Timer()
    {
        time += Time.deltaTime;

        if (time >= 3f)
            Trap();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            _obj = this.transform.gameObject;//_obj�� ���� ���� ��!
            pos = new Vector3(
                _obj.transform.position.x, 
                _obj.transform.position.y, 
                _obj.transform.position.z);

            isOn = true;
            timerOn = true;
        }
    }


    private void  Trap()//�Ϲ� �� -> Ʈ�� �Ǿ�
    {
        timerOn = false;
        Debug.Log("trap");
        Instantiate(_objTrap, pos, Quaternion.identity);//���� �ִ� ���� Ʈ�� ����

        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer�� �ٲ���
        _obj.transform.position = new Vector3(999, 999, 999);//��Ҵ� �� ���ְ�~

        InvokeRepeating("decreaseHealth", 0.2f, 2f);
    }

    private int count = -1;//ü�±����� ���̴� ����

    private void decreaseHealth()
    {
        count = -1;
        player.health -= 1;//�Ӹ� ���� ü�� �����
        popUp();
        Debug.Log(player.health);
        //while (player.health > 0 && count == -1)
        //{
        //    count++;
            
        //    spr.material.color = Color.red;

        //}
    }

    private void popUp()//ü�� -1 �Ӹ� ���� ���
    {
        overHead.transform.position = new Vector3
            (playerPos.position.x, playerPos.position.y+10f, playerPos.position.z);

        Invoke("disappear", 1.5f);

    }

    private void disappear()
    {
        overHead.SetActive(false);//�̰� �ٽ� Ǯ�����
    }

    
}
