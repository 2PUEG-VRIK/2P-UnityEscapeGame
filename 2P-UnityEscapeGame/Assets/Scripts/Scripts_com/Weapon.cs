using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    public void init()
    {
        trailEffect.enabled = false;

    }
    public void Use()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }
    IEnumerator Swing()
    {
        trailEffect.enabled = true;
        
        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.2f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.6f);
        trailEffect.enabled = false;
    }
}
