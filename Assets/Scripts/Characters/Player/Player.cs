using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour  //플레이어의 대대분들의 내용을 여기서 선언한다.
{

    public ForceReceiver ForceReceiver { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    private PlayerStateMachine stateMachine;


    public GameObject weapon;
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }

    private void Awake()  //각자의 초기화 진행
    {
        AnimationData.Initialize();

        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();

        stateMachine = new PlayerStateMachine(this);
        ForceReceiver = GetComponent<ForceReceiver>();
    }

    private void Start()
    {
      //  Animator.SetTrigger("Dodge");
     //   Cursor.lockState = CursorLockMode.Locked;  //커서를 사라지도록
        stateMachine.ChangeState(stateMachine.IdleState); //처음의 상태는 idle
    }

    private void Update()
    {
        stateMachine.HandleInput(); // 입력값
        stateMachine.Update();  //그냥 업데이트값
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate(); //물리연산 값
    }
}
