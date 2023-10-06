using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBaseState : IState //구성하면서 필요한 베이스
{
    protected PlayerStateMachine stateMachine; //역참조를 해야함
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundedData;
    }

    public virtual void Enter() //값 스테이어들이 가져가서 원하는 동작으로 재구현해야하기 때문에 버츄얼로 선언함.
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();//역참조     // 많이 사용하기 때문에 캐싱하는 것도 좋음
    }

    private void Move()//실제 이동처리
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private Vector3 GetMovementDirection() //카메라가 바라보는 방향으로 이동하기
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward; //메인카메라 정면
        Vector3 right = stateMachine.MainCameraTransform.right; //메인카메라의 오른쪽

        forward.y = 0; //y값을 제거 해야지 땅바닥을 안본다
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 movementDirection) 
    {
        float movementSpeed = GetMovemenetSpeed();
        stateMachine.Player.Controller.Move(
            (movementDirection * movementSpeed) * Time.deltaTime
            );  //이동방향과 스피드를 곱해주기
    }

    private void Rotate(Vector3 movementDirection) //회전
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private float GetMovemenetSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected virtual void AddInputActionsCallbacks()  //공용으로 사용되는 것들에 대하여 사용된다, 골백들을 연결하거나 해제
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled += OnMovementCanceled;
        input.PlayerActions.Run.started += OnRunStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;

    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }



}