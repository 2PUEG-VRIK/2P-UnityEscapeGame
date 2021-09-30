using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;
    bool cooldown = false;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.gameObject.name + "::" + this.cooldown.ToString());
        if (cooldown)
        {
            return;
        }
        if (target != null)
        {
            if (target.GetComponent<Teleport>() != null)
            {
                target.GetComponent<Teleport>().cooldown = true;
            }
            if (other.gameObject.tag == "Player")
            {
                Vector3 position = target.transform.position;
                position.y += target.transform.localScale.y;
                other.transform.position = position;
            }
            if(target.GetComponent<Teleport>() != null)
            {
                this.StartCoroutine("toggleCooldown");
            }
        }
    }
    public IEnumerator toggleCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        target.GetComponent<Teleport>().cooldown = false;
        StopCoroutine("toggleCooldown");
    }
}
