using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag = 0.3f; //저항

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;//수직의 힘과 impact는 무언가의 영향

    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded) //땅에 붙어있으면
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime; //9.7중력사용
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;//아니면 계속해서 중력증가
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