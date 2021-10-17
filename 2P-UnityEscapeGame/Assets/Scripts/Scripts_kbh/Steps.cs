using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    Vector3 pos;//밟은 땅의 위치
    GameObject _obj;//밟은 땅
    public GameObject _objTrap;//생성할 트랩
    Man player;//나~
    SpriteRenderer spr;
    private float time = 0f;//징검다리에 있는 시간 재기(2초)
    bool isOn = false;//징검다리에 올라와있따
    bool timerOn = false;
    GameObject overHead;//머리위에 체력 -1 뜨는거
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
            if (Input.GetKeyDown(KeyCode.Space)) //collider Exit 역할
            {
                time = 0f;
                Debug.Log("이거 아녀? 스페이스바눌렀읐때");
                timerOn = false;
                isOn = false;
                CancelInvoke("decreaseHealth");
            }
            else if (timerOn)
                Timer();
        }


        if (player.health == 0)
            Debug.Log("끝내삼");

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
            _obj = this.transform.gameObject;//_obj는 내가 밟은 땅!
            pos = new Vector3(
                _obj.transform.position.x, 
                _obj.transform.position.y, 
                _obj.transform.position.z);

            isOn = true;
            timerOn = true;
        }
    }


    private void  Trap()//일반 블럭 -> 트랩 되어
    {
        timerOn = false;
        Debug.Log("trap");
        Instantiate(_objTrap, pos, Quaternion.identity);//내가 있던 곳에 트랩 생성

        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer로 바꿔줌
        _obj.transform.position = new Vector3(999, 999, 999);//밟았던 곳 없애고~

        InvokeRepeating("decreaseHealth", 0.2f, 2f);
    }

    private int count = -1;//체력깎을때 쓰이는 변수

    private void decreaseHealth()
    {
        count = -1;
        player.health -= 1;//머리 위로 체력 띄우자
        popUp();
        Debug.Log(player.health);
        //while (player.health > 0 && count == -1)
        //{
        //    count++;
            
        //    spr.material.color = Color.red;

        //}
    }

    private void popUp()//체력 -1 머리 위에 띄워
    {
        overHead.transform.position = new Vector3
            (playerPos.position.x, playerPos.position.y+10f, playerPos.position.z);

        Invoke("disappear", 1.5f);

    }

    private void disappear()
    {
        overHead.SetActive(false);//이거 다시 풀어야해
    }

    
}
