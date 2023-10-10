using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.GroundParameterHash); //그라운드에 관한 애니메이션 키기 
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!stateMachine.Player.Controller.isGrounded
        && stateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.MovementInput == Vector2.zero)//입력이 없으면
        {
            return;
        }

        stateMachine.ChangeState(stateMachine.IdleState);  //입력처리가 들어왔다가 최소가 된다면

        base.OnMovementCanceled(context);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.WalkState);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.JumpState);
    }

    protected override void OnBlockStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.BlockState);
    }
    protected override void OnDodgeStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.DodgeState);
    }
    protected override void OnSkillStarted(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.SkillState);
    }
    protected virtual void OnAttack()
    {
        stateMachine.ChangeState(stateMachine.ComboAttackState);
    }

}