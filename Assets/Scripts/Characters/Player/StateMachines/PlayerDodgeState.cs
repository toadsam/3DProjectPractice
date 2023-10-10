using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerGroundedState
{

    public PlayerDodgeState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        //stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        Debug.Log(1);
        stateMachine.Player.Animator.SetTrigger("DDodge");
        //StartAnimation(stateMachine.Player.AnimationData.DodgeParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        //StopAnimation(stateMachine.Player.AnimationData.DodgeParameterHash);
    }

    public override void Update()
    {
        base.Update();
        //stateMachine.Player.transform.position += new Vector3()
        //if (stateMachine.MovementInput != Vector2.zero)  //�̵����Ͼ��
        //{
        //    OnMove(); //����Ʈ�� ���̵�� �ٲ۴�.
        //    return;
        //}
    }
}
