using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f; //����

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;//������ ���� impact�� ������ ����

    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded) //���� �پ�������
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime; //9.7�߷»��
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;//�ƴϸ� ����ؼ� �߷�����
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}