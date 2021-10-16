using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    BoxCollider boxcollider;
    Rigidbody rigid;
    Vector3 pos;//¹âÀº ¶¥ÀÇ À§Ä¡
    GameObject _obj;//¹âÀº ¶¥
    public GameObject _objTrap;//»ý¼ºÇÒ Æ®·¦
    Man player;//³ª~
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
            _obj = this.transform.gameObject;//_obj´Â ³»°¡ ¹âÀº ¶¥!
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



    IEnumerator Trap()//ÀÏ¹Ý ºí·° -> Æ®·¦ µÇ¾î
    {
        yield return new WaitForSeconds(1f);

        Instantiate(_objTrap, pos, Quaternion.identity);//³»°¡ ÀÖ´ø °÷¿¡ Æ®·¦ »ý¼º
        _obj.transform.position=new Vector3(999,999,999);//¹â¾Ò´ø °÷ ¾ø¾Ö°í~

        _objTrap.transform.localScale = _obj.transform.localScale;
        _objTrap.tag = "Floor";
        _objTrap.gameObject.layer = 7;//Floor layer·Î ¹Ù²ãÁÜ
            StartCoroutine(decreaseHealth());



    }
    private int count = -1;
    IEnumerator decreaseHealth()//Æ®·¦À§¿¡ ÀÖ´Âµ¿¾È Ã¼·Â ±ð¾Æ~ & »ç¶÷ »ö±ò »¡°£»öÀ¸·Î ³õ±â
    {
        yield return new WaitForSeconds(3f);
        while (player.health > 0 && count==-1)
        {
            count++;
            player.health -= 1;//¸Ó¸® À§·Î Ã¼·Â ¶ç¿ìÀÚ
            Debug.Log(player.health);
            spr.material.color = Color.red;
            StartCoroutine(decreaseHealth());
            
            
        }
    }
}
