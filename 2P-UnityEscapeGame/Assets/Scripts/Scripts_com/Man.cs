using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
    public float speed = 25;

    float hAxis;
    float vAxis;

    Vector3 moveVec;
    Vector3 preVec;

    Animator anim;
    Rigidbody rigid;

    bool isSwap;        // 스왑할땐 아무런 뭣도 안하도록 함.
    bool isBump;
    bool isJump;
    bool sDown1;        //무기바꾸는 변수
    bool sDown2;
    bool sDown3;
    bool iDown;
    bool jDown;
    bool istoWALL;      // 벽 통과 못하게 막는 플래그   
    bool istoObj;

    // 무기 부분
    public GameObject[] weapons; // 이게 손에 들려있는 가려진 무기
    public bool[] hasWeapons;
    Weapon equipWeapon; // 무기의 스크립트를 가져오겠다는 거임.
    //장착중인 weapon

    int equipWeaponIndex = -1;

    public int health;
    public int maxHealth;

    public int ammo;
    public int maxAmmo;

    bool AttackDown; // 공격키
    GameObject nearObject;  //트리거 된 아이템 저장하는 변수

    bool isFireReady = true;
    float fireDelay;

    IEnumerator enu1; //ladder에 필요
    private bool isLadder; //사다리 오르락내리락할 때 필요한 변수(2021-10-03, 김보)

    void Start()
    {

    }

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Attack();
        Swap();
        Interaction();
    }
    private void FixedUpdate()
    {
        FreezeRotation();   // 플레이어가 탄피나 그런거에 닿으면 회전을 하기 시작.. 그거 없애려고 해주는것임
        StoptoWall();       // 벽 or 박스 통과 방지


        //if (isLadder)
        //{
        //    rigid.useGravity = false;

        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //        34 82 69
        //       transform.Translate()
        //            // transform.Translate(new Vector3(0, speed * 1f * Time.deltaTime, speed* -0.5f * Time.deltaTime).normalized);
        //    else if(Input.GetKeyDown(KeyCode.DownArrow))
        //    {34 3 28
                
        //        //transform.Translate(new Vector3(0, speed * -1f * Time.deltaTime, speed * -2f * Time.deltaTime).normalized);
        //    }

        //}
    }

    

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void StoptoWall()
    {
        // 2021-09-27 원종진 수정
        // 플레이어에서 길이 3만큼의 Raycast 쐈을 때 Wall 레이어와 닿으면 isBorder ON
        istoWALL = Physics.Raycast(transform.position, transform.forward, 5, LayerMask.GetMask("Wall"));
        istoObj = Physics.Raycast(transform.position, transform.forward, 3.5f, LayerMask.GetMask("Box"));
    }

    void GetInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        jDown = Input.GetButtonDown("Jump");
        //   iDown = Input.GetButtonDown("Interaction");
        AttackDown = Input.GetButton("Attack");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
    }

    void Move()
    {
        if (isBump || isSwap)
        {
            return;
        }

        if (!isFireReady)
            moveVec = Vector3.zero;

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isJump)
        {
            moveVec *= 0.5f;
        }

        if (moveVec != Vector3.zero)
        {
            preVec = moveVec;
        }

        //if (istoWALL)       // Wall Layer과 충돌하지 않을 때만 이동 가능하게 설정
        //    transform.position += moveVec * 0 * Time.deltaTime;

        //else if (!istoWALL && istoObj)
        //    transform.position += moveVec * speed * 0.375f * 1f * Time.deltaTime;

        //else
        //    transform.position += moveVec * speed * 1f * Time.deltaTime;

        if (!istoWALL)       // Wall Layer과 충돌하지 않을 때만 이동 가능하게 설정 
            transform.position += moveVec * speed * 1f * Time.deltaTime;

        anim.SetBool("isWalk", (moveVec != Vector3.zero));  // 속도가 0이 아니면 걸어라.
    }

    void Turn()
    {
        // 가는 방향 보기.
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        // 점프 키 눌렀을 때 아이템 있으면 아이템 먹음.
        if (jDown)
        {
            if (nearObject != null)
            {
                // 아이템 먹기
                Destroy(nearObject);

                equipWeapon = weapons[equipWeaponIndex].GetComponent<Weapon>();
                equipWeapon.gameObject.SetActive(true);
                equipWeapon.init();


            }
            else if (!isJump)
            {
                // 점프는 그냥 위로 속도주기.
                rigid.AddForce(Vector3.up * 200, ForceMode.Impulse);

                anim.SetTrigger("Jump");
                isJump = true;
            }
        }
    }

    void Swap()
    {

        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if ((sDown1 || sDown2 || sDown3))//점프할때 금지되어있었음
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);

            anim.SetTrigger("Swap");
            isSwap = true;

            Invoke("SwapOut", 0.5f);

        }
    }


    void SwapOut()
    {
        isSwap = false;
    }

    void Attack()
    {
        if (equipWeapon == null) //먹은 무기가 없으면
        {
            return;
        }

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay; // 공격속도(쿨타임)보다 파이어딜레이(지난 시간)가 크면 된다고..?

        if (AttackDown && isFireReady && !isSwap)
        {
            equipWeapon.Use();
            anim.SetTrigger(equipWeapon.type == Weapon.Type.Melee ? "Swing" : "Shot");
            if (equipWeapon.type != Weapon.Type.Melee)//총이면 총알 -1
            {
                ammo--;
                if (ammo <= 0)
                    ammo = 0;
            }
            fireDelay = 0; //다음 공격까지 기다리도록
        }
    }

    void Interaction()
    {
        if (nearObject != null) //&&iDown
        {
            if (nearObject.tag == "Item")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex;
                if (item.type == Item.Type.Weapon)
                {
                    weaponIndex = item.value;
                    hasWeapons[weaponIndex] = true;
                }
            }
        }
    }

 
    public int check = -1;
    //private bool isLadder; //사다리 오르락내리락할 때 필요한 변수(2021-10-03, 김보)

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Weapon:
                    break;
                case Item.Type.Coin:
                    check = 1;

                    //this.transform.localScale *= 2;
                    break;
                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;

                case Item.Type.Ammo:
                    ammo += item.value;
                    if (ammo > maxAmmo)
                        ammo = maxAmmo;
                    break;
                    //case Item.Type.Key:


            }
            Interaction();
            Destroy(other.gameObject);//원래 exit에 있었음

        }
        else if (other.tag == "Enemy")
        {
            health--;
            //Vector3 reactVec = transform.forward * Random.Range(-15, -20) + Vector3.up * Random.Range(5,10);
            //rigid.AddForce(reactVec*3, ForceMode.Impulse);

            if (health <= 0)
                Quit();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        // 바닥 닿으면 다시 점프 가능상태로 바꿔주기.
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Box")
        {
            isJump = false;
        }

        //if (collision.gameObject.tag == "Ladder")//사다리
      // {

        //    isLadder = true;
        //    Debug.Log("사다리에 닿았다");
        //    if (Input.GetKey(KeyCode.UpArrow))
       //     {
         //       Debug.Log("화살표 눌러짐");
 
       //         enu1 = LadderUp(transform.position, new Vector3(34, 82, 69), 2f);
        //        //StartCoroutine(LadderUp, transform.position, new Vector3(34,82,69), 2f);
       //         StartCoroutine(enu1);
        //    }
       // }
     }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "Ladder")
        {
           // isLadder = false;
            rigid.useGravity = true;
            StopCoroutine(enu1);

        }
    }

    private IEnumerator LadderUp(Vector3 startPos, Vector3 targetPos, float duration)
    {
        Debug.Log("코루틴 실행");
        float timer = 0f;

        // 이동 시작 위치 설정
        Vector3 position = startPos;
        //rectTransform.anchoredPosition = position;

        // 시간에 따른 위치 설정
        while (timer < duration)
        {
            timer += Time.deltaTime;

            position.x = Mathf.Lerp(startPos.x, targetPos.x, timer / duration);
            position.y = Mathf.Lerp(startPos.y, targetPos.y, timer / duration);
            position.x = Mathf.Lerp(startPos.z, targetPos.z, timer / duration);

            transform.localPosition = position;

            yield return null;
        }

        // 이동 종료 위치 설정
        position = targetPos;
        transform.localPosition = position;

    }
    void Bump()
    {
        //anim.SetTrigger("Bump");
        //isBump = true;
        //transform.position += preVec * -7;

        //Invoke("BumpOut", 1.5f);
    }

    void BumpOut()
    {
        //isBump = false;
    }


    //void OnGUI()
    //{
    //    //무슨 키 입력했는지 알려주는 코드.
    //    Event e = Event.current;
    //    if (e.isKey)
    //    {
    //        Debug.Log("Detected a keyboard event!" + e.keyCode);
    //    }

    //}

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
