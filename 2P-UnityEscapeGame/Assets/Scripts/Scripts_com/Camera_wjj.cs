using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_wjj : MonoBehaviour
{
    public Transform target;
    public Vector3 offset; // ������ (ī�޶�� �÷��̾� ������ �Ÿ�)

    void Update()
    {
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}
