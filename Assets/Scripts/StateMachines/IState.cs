using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState //인터페이스 구현이니 내부구현 x
{
    public void Enter();
    public void Exit();
    public void HandleInput(); //입력처리를 할 떄
    public void Update();
    public void PhysicsUpdate();

}
