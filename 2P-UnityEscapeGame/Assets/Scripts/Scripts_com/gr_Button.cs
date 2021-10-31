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
    Material mat;

    private bool isPushed;

    void Start()
    {
        //mat = GetComponent<MeshRenderer>().material;
        isPushed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPushed)
        {  
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Box" || other.gameObject.tag == "Boxsj")       
            {
                transform.position += new Vector3(0, -0.5f, 0);
                isPushed = true;

                Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();
                rigid.AddForce(gr_buttonPos.forward * 50, ForceMode.VelocityChange);
                // rigid.velocity = gr_buttonPos.forward * 30;
            }   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPushed)
        {
            transform.position += new Vector3(0, 0.5f, 0); 
            isPushed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }
}
