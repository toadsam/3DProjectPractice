using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public abstract class StateMachine
{
    protected IState currentState; //������ ����

    public void ChangeState(IState newState) //�ٲ����
    {
        currentState?.Exit();//������ ���¿��� ������

        currentState = newState; // ���ο� ���¹ޱ�

        currentState?.Enter();// ���ο���� ����
    }

    public void HandleInput()  //���罺����Ʈ�� �ڵ��� ����ǵ���
    {
        currentState?.HandleInput();
    }

    public void Update()  // ��������� ������Ʈ
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()  //��������� ������������Ʈ
    {
        currentState?.PhysicsUpdate();
    }
}
