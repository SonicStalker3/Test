using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody),typeof(CharacterController))]
public class Player : StatsEntity
{
    [Header("Base Values")]
    public float speed = 6.0f;
    public float jumpForce = 8.0f;
    public float rotateSpeed = 0.8f;
    [Range(5f,50f),SerializeField] 
    private float gravity = 20.0f;
    [Range(0,20f)]
    public float MaxAttackDistance = 5;
    [Header("DialogGameObjects"),Space]
    public GameObject DiagPanel;
    public GameObject HistoryPanel;
    public GameObject HistoryPanelView;
    public bool isControll = true;


    //private IControllable _controllable;
    //private InputSys _inputSys;
    //PlayerInput _input;
    
    private Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;
    private Transform playerCamera;
    private ParticleSystem _dustEffect;

    private PlayerInput _input;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private bool jumpCond;
    private Camera _camera;
    [SerializeField] private bool isAttacking;
    [Range(1, 10)]
    [SerializeField] private int maxAttackRate = 5;

    private void Start()
    {
        _camera = Camera.main;
        health = MaxHealth;
        _input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>().transform;
        //_inputSys = GetComponent<InputSys>();
        _dustEffect = GetComponentInChildren<ParticleSystem>();
        //_input._input.actions["Jump"].pre+= Jump;
    }

    void Update()
    {
        //_lookInput = _input.actions["Rotate"].ReadValue<Vector2>();
        if (isControll)
        {
            // if (_input.currentActionMap.ToString() != ActionMaps.GamePlay.ToString())
            // {
            //     _input.SwitchCurrentActionMap(ActionMaps.GamePlay.ToString());
            // }
            if (InputSys.CurrentActionMap != ActionMaps.GamePlay)
            {
                InputSys.SwitchActionMap(ActionMaps.GamePlay);
            }
            
            Move();
            if(InputSys.AttackBtn)
            {
                Ray ray = _camera!.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.distance <= MaxAttackDistance)
                    {
                        hit.collider.TryGetComponent(out Entity entity);
                        /*Debug.Log($"{entity} {hit.collider.name}");*/
                        entity?.TakeDamage(baseDamage);
                    }
                    
                }
            }
            
            HistoryPanel.SetActive(false);
            DiagPanel.SetActive(false);
        }
        else
        {
            if (InputSys.CurrentActionMap != ActionMaps.Dialog)
            {
                InputSys.SwitchActionMap(ActionMaps.Dialog);
            }
            Cursor.lockState = CursorLockMode.Confined;
            moveDirection = Vector3.zero;
            //HistoryPanel.SetActive(true);
            DiagPanel.SetActive(true);
        }
        //Debug.Log();
        
        if (moveDirection == Vector3.zero) _dustEffect.Stop();
        else _dustEffect.Play();
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        

    }
    
    private IEnumerator AttackCorut()
    {
        /*isAttacking = true;
        yield return new WaitForSeconds(1 / maxAttackRate);
        AttackEnemy();
        isAttacking = false;*/
        return null;
    }
    
    private void Move()
    {
        Cursor.lockState = CursorLockMode.Locked;
        /*if (playerModel)
        {
            playerModel.Rotate(0, _inputSys.lookInput.x * rotateSpeed, 0);
            playerCamera.Rotate(-_inputSys.lookInput.y * rotateSpeed, _inputSys.lookInput.x * rotateSpeed , 0);
        }*/
        transform.Rotate(0, InputSys.LookInput.x * rotateSpeed, 0);
        playerCamera.Rotate(-InputSys.LookInput.y * rotateSpeed, 0, 0);
        
        if (playerCamera.localRotation.eulerAngles.y != 0)
        {
            playerCamera.Rotate(InputSys.LookInput.y * rotateSpeed, 0, 0);
        }
        
        moveDirection = new Vector3(InputSys.MoveInput.x * speed, moveDirection.y, InputSys.MoveInput.y * speed);
        if (controller.isGrounded)
        {
            if (InputSys.JumpInput)
            {
                moveDirection.y = jumpForce;
            }
            else moveDirection.y = 0;
        }
    }

    public void OnEndDialog()
    {
        isControll = true;
    }
}