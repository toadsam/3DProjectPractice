using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; } // ��ǲ�׼��� Ŭ���� �޾ƿ���
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; } //�츮�� ������ �׼� �޾ƿ���

    private void Awake()
    {
        InputActions = new PlayerInputActions();

        PlayerActions = InputActions.Player;
    }
    private void OnEnable()  //��ǲ�׼��� �������ִ� ���̴� �÷��̾ �����µ� �۵��ϰų� �׷� �͵��� �����ϱ� ���ؼ�
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}
