using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerGroundedState
{
    
    public PlayerSkillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    // Start is called before the first frame update
    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0;
        base.Enter();
        stateMachine.Player.weapon.transform.localScale = new Vector3(4,4,4);
        stateMachine.Player.Animator.SetTrigger("Skill");
       // StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        stateMachine.Player.weapon.transform.localScale = new Vector3(1, 1, 1);
        base.Exit();

        //StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }
}
