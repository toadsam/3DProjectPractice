using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerGroundedState
{
    // Start is called before the first frame update
    public PlayerBlockState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        Debug.Log(1);
        StartAnimation(stateMachine.Player.AnimationData.BlockParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.BlockParameterHash);
    }

    public override void Update()
    {
        base.Update();
        //stateMachine.Player.transform.position += new Vector3()
        if (stateMachine.MovementInput != Vector2.zero)  //�̵����Ͼ��
        {
            OnMove(); //����Ʈ�� ���̵�� �ٲ۴�.
            return;
            }
        }
}
