using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public abstract class StateMachine
{
    protected IState currentState; //현재의 상태

    public void ChangeState(IState newState) //바뀐상태
    {
        currentState?.Exit();//그전의 상태에서 나가기

        currentState = newState; // 새로운 상태받기

        currentState?.Enter();// 새로운상태 시작
    }

    public void HandleInput()  //현재스테이트의 핸들이 실행되도록
    {
        currentState?.HandleInput();
    }

    public void Update()  // 현재상태의 업데이트
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()  //현재상태의 피직스업데이트
    {
        currentState?.PhysicsUpdate();
    }
}
