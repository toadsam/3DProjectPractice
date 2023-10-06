using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; } // 인풋액션의 클래스 받아오기
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; } //우리가 만들어온 액션 받아오기

    private void Awake()
    {
        InputActions = new PlayerInputActions();

        PlayerActions = InputActions.Player;
    }
    private void OnEnable()  //인풋액션을 관리해주는 것이다 플레이어가 꺼졌는데 작동하거나 그런 것들을 방지하기 위해서
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
