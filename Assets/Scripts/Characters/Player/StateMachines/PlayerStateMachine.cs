using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    // States  //상태를 나타낸다
    public PlayerIdleState IdleState { get; }
    public PlayerWalkState WalkState { get; }
    public PlayerRunState RunState { get; }

    public PlayerJumpState JumpState { get; }
    public PlayerFallState FallState { get; }

    public PlayerDodgeState DodgeState { get; }

    public PlayerBlockState BlockState { get; }

    public PlayerComboAttackState ComboAttackState { get; }

    public PlayerSkillState SkillState { get; }
    public Player Player { get; }

    // States

    // 

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }

    public Transform MainCameraTransform { get; set; }

    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        ComboAttackState = new PlayerComboAttackState(this);
        DodgeState = new PlayerDodgeState(this);
        BlockState = new PlayerBlockState(this);
        SkillState  = new PlayerSkillState(this);
        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundedData.BaseSpeed;
        RotationDamping = player.Data.GroundedData.BaseRotationDamping;
    }
}