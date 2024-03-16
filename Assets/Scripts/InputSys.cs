using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps: byte
{
    GamePlay=0,
    Dialog
}

public class InputSys : MonoBehaviour
{
    public PlayerInput _input;

    private ActionMaps _actionMap;
    
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private bool _jumpInput;
    private bool _historyBtn;
    private bool _nextBtn;
    private bool _attackBtn;
    public Vector2 moveInput => _moveInput;//{ get => _moveInput; }
    public Vector2 lookInput => _lookInput;
    public bool jumpInput => _jumpInput;
    
    private float _nextMessageHoldTime;
    public bool nextBtn => _nextBtn;
    public bool historyBtn => _historyBtn;
    public bool attackBtn => _attackBtn;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _actionMap = ActionMaps.GamePlay;
        
        _input.actions["Jump"].performed += OnJump;
    }

    private void Update()
    {
        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        _lookInput = _input.actions["Rotate"].ReadValue<Vector2>();
        _nextBtn = _input.actions["NextMessage"].triggered;
        _historyBtn = _input.actions["History"].triggered;
        _attackBtn = _input.actions["Attack"].triggered;
        //_jumpInput = _input.actions["Jump"]();
        //_attackBtn = Input.GetMouseButtonDown(0);
        //_nextBtn = Input.GetButtonUp("Jump");
        //_historyBtn = Input.GetButtonUp("History");
        //_jumpInput = _input.actions["Jump"].triggered;

    }


    public void OnJump(InputAction.CallbackContext context)
    {
        _jumpInput = context.ReadValueAsButton();
    }

    public void SwitchActionMap(ActionMaps actionMap)
    {
        _input.SwitchCurrentActionMap(actionMap.ToString());
        _actionMap = actionMap;
    }

    //public void Bind()
    //{
        // var rebind = new RebindingOperation()
        //     .WithAction(myAction)
        //     .WithBindingGroup("Gamepad")
        //     .WithCancelingThrough("<Keyboard>/escape");
        //
        // rebind.Start();
    //}
    
    public ActionMaps CurrentActionMap => _actionMap;
}
