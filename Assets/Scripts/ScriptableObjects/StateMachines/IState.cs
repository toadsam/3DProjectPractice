using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState //�������̽� �����̴� ���α��� x
{
    public void Enter();
    public void Exit();
    public void HandleInput(); //�Է�ó���� �� ��
    public void Update();
    public void PhysicsUpdate();

}
