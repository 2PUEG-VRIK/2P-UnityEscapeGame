using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gr_Button : MonoBehaviour
{
    // �ʷϹ�ư ������ ��
    // (1) �÷��̾ �ڽ��� �д� �������� �ڽ� ������ �̵�
    // (2) �÷��̾ �����̴� �������� �÷��̾� ������ �̵�
    // ��� : �� �� �÷��̾ �����̴� �������� �̵������ָ� ��

    public Transform gr_buttonPos;     // �÷��̾ �����̴� ���� �������� ���� empty. �÷��̾�ȿ� ������ �ٸ� ���� �ʿ��ϸ� �̰� �־ ���� ��.
    ParticleSystem particle;
    Material mat;

    bool isPushed = false;

    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.gameObject.SetActive(false);
        mat = GetComponent<MeshRenderer>().material;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isPushed)
        {
            transform.position += new Vector3(0, -0.99f, 0);
            isPushed = true;

            if(other.gameObject.tag == "Player")        // �÷��̾ �ʷϹ�ư ������ ���� �������� x30
            {
                Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();

                rigid.velocity = gr_buttonPos.forward * 30;
            }
            
            else if (other.gameObject.tag == "Box")   // �ڽ��� �ʷϹ�ư ������ �÷��̾ �ڽ��� �д� �������� x50
            {
                Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();

                rigid.velocity = gr_buttonPos.forward * 50;
            }

            particle.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPushed)
        {
            transform.position += new Vector3(0, +0.99f, 0);
            isPushed = false;

            particle.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }
}
