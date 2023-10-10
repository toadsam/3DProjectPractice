using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour  //�÷��̾��� ���е��� ������ ���⼭ �����Ѵ�.
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

    private void Awake()  //������ �ʱ�ȭ ����
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
     //   Cursor.lockState = CursorLockMode.Locked;  //Ŀ���� ���������
        stateMachine.ChangeState(stateMachine.IdleState); //ó���� ���´� idle
    }

    private void Update()
    {
        stateMachine.HandleInput(); // �Է°�
        stateMachine.Update();  //�׳� ������Ʈ��
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate(); //�������� ��
    }
}
