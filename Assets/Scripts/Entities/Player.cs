using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody),typeof(CharacterController))]
public class Player : Entity
{
    [Header("Base Values")]
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
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
    private InputSys _inputSys;
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

    private void Start()
    {
        _camera = Camera.main;
        _health = MaxHealth;
        _input = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>().transform;
        _inputSys = GetComponent<InputSys>();
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
            if (!_inputSys && _inputSys.CurrentActionMap != ActionMaps.GamePlay)
            {
                _inputSys.SwitchActionMap(ActionMaps.GamePlay);
            }
            
            Move();
            if(_inputSys.attackBtn)
            {
                Ray ray = _camera!.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.distance <= MaxAttackDistance)
                    {
                        hit.collider.TryGetComponent(out IDamageble entity);
                        entity?.Damage(baseDamage);
                    }
                    
                }
            }
            
            HistoryPanel.SetActive(false);
            DiagPanel.SetActive(false);
        }
        else
        {
            if (!_input && _inputSys.CurrentActionMap != ActionMaps.Dialog)
            {
                _inputSys.SwitchActionMap(ActionMaps.Dialog);
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

    private void Move()
    {
        Cursor.lockState = CursorLockMode.Locked;
        /*if (playerModel)
        {
            playerModel.Rotate(0, _inputSys.lookInput.x * rotateSpeed, 0);
            playerCamera.Rotate(-_inputSys.lookInput.y * rotateSpeed, _inputSys.lookInput.x * rotateSpeed , 0);
        }*/
        transform.Rotate(0, _inputSys.lookInput.x * rotateSpeed, 0);
        playerCamera.Rotate(-_inputSys.lookInput.y * rotateSpeed, 0, 0);
        
        if (playerCamera.localRotation.eulerAngles.y != 0)
        {
            playerCamera.Rotate(_inputSys.lookInput.y * rotateSpeed, 0, 0);
        }
        
        moveDirection = new Vector3(_inputSys.moveInput.x * speed, moveDirection.y, _inputSys.moveInput.y * speed);
        if (controller.isGrounded)
        {
            if (_inputSys.jumpInput)
            {
                moveDirection.y = jumpSpeed;
            }
            else moveDirection.y = 0;
        }
    }

    public void OnEndDialog()
    {
        isControll = true;
    }
}