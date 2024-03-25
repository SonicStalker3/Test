using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps: byte
{
    GamePlay=0,
    Dialog
}


public static class InputSys
{
    private static Vector2 _moveInput;
    private static Vector2 _lookInput;
    private static bool _jumpInput;
    private static bool _nextBtn;
    private static bool _historyBtn;
    private static bool _attackBtn;
    
    private static InputAction _navigateUI;
    
    private static ActionMaps _actionMapEnum;
    private static InputActionMap _currentActionMap;

    private static InputActionMap _gameplayActionMap;
    private static InputActionMap _dialogActionMap;
    
    public static float cursor_sensitivity = 2;
    private static readonly InputActionAsset _gameplayControls;

    static InputSys()
    {
        _gameplayControls = Resources.Load<InputActionAsset>("GamePlayControlls");
        
        _gameplayActionMap = _gameplayControls.FindActionMap("GamePlay");
        _dialogActionMap = _gameplayControls.FindActionMap("Dialog");
        _navigateUI = _gameplayControls.FindActionMap("UI").FindAction("Navigate");

        _gameplayActionMap.FindAction("Jump").performed += OnJump;
        
        SwitchActionMap(ActionMaps.GamePlay);
    }
    public static void OnJump(InputAction.CallbackContext context)
    {
        _jumpInput = context.ReadValueAsButton();
    }
    
    public static void Update()
    {
        _moveInput = _gameplayActionMap.FindAction("Move").ReadValue<Vector2>();
        _lookInput = _gameplayActionMap.FindAction("Rotate").ReadValue<Vector2>() * cursor_sensitivity;
        //_jumpInput = _gameplayActionMap.FindAction("Jump").triggered;
        _attackBtn = _gameplayActionMap.FindAction("Attack").triggered;
        
        _nextBtn = _dialogActionMap.FindAction("NextMessage").triggered;
        _historyBtn = _dialogActionMap.FindAction("History").triggered;
        
        /*_moveInput = _input.actions["Move"].ReadValue<Vector2>();
        _lookInput = _input.actions["Rotate"].ReadValue<Vector2>() * cursor_sensitivity;
        _nextBtn = _input.actions["NextMessage"].triggered;
        _historyBtn = _input.actions["History"].triggered;
        _attackBtn = _input.actions["Attack"].triggered;*/
    }

    public static Dictionary<string, InputAction> ActionsList()
    {
        Dictionary<string, InputAction> actionDict = new Dictionary<string, InputAction>();

        foreach (var actionMap in _gameplayControls.actionMaps)
        {
            foreach (var action in actionMap.actions)
            {
                string key = $"{actionMap.name}/{action.name}";
                actionDict[key] = action;
            }
        }

        /*
        foreach (var VARIABLE in actionDict)
        {
            Debug.Log(VARIABLE.Key);
        }*/
        
        return actionDict;
    }

    public static void SwitchActionMap(ActionMaps actionMap)
    {
        if (_currentActionMap != null)
        {
            _currentActionMap.Disable();
        }

        switch (actionMap)
        {
            case ActionMaps.GamePlay:
                _currentActionMap = _gameplayActionMap;
                break;
            case ActionMaps.Dialog:
                _currentActionMap = _dialogActionMap;
                break;
            default:
                _currentActionMap = _gameplayActionMap;
                break;
        }

        _currentActionMap.Enable();
        _actionMapEnum = actionMap;
    }

    public static void SwitchActionMap(ActionMaps[] actionMaps)
    {
        if (_currentActionMap != null)
        {
            _currentActionMap.Disable();
        }

        foreach (ActionMaps map in (ActionMaps[])Enum.GetValues(typeof(ActionMaps)))
        {
            switch (map)
            {
                case ActionMaps.GamePlay:
                    _currentActionMap = _gameplayActionMap;
                    break;
                case ActionMaps.Dialog:
                    _currentActionMap = _dialogActionMap;
                    break;
                default:
                    _currentActionMap = _gameplayActionMap;
                    break;
            }

            if (_currentActionMap != null) _currentActionMap.Enable();
            _actionMapEnum = map;
        }
    }
    public static void Bind(InputAction Action, string Type = "Keyboard")
    {
     var rebind = new InputActionRebindingExtensions.RebindingOperation()
         .WithAction(Action)
         .WithBindingGroup("Gamepad")
         .WithCancelingThrough("<Keyboard>/escape");
     rebind.Start();
    }

    public static Vector2 MoveInput => _moveInput;
    public static Vector2 LookInput => _lookInput;
    public static bool JumpInput => _jumpInput;
    public static bool NextBtn => _nextBtn;
    public static bool HistoryBtn => _historyBtn;
    public static bool AttackBtn => _attackBtn;
    public static ActionMaps CurrentActionMap => _actionMapEnum;
    public static InputAction NavigateUI => _navigateUI;
}


/*
public class InputSys: MonoBehaviour
{
    public PlayerInput _input;
    /*[SerializeField]
    private InputActionAsset _altInput;#1#
    private ActionMaps _actionMap;
    
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private bool _jumpInput;
    private bool _historyBtn;
    private bool _nextBtn;
    private bool _attackBtn;
    private static InputAction _navigateUI;
    public Vector2 moveInput => _moveInput;//{ get => _moveInput; }
    public Vector2 lookInput => _lookInput;
    public bool jumpInput => _jumpInput;
    public bool nextBtn => _nextBtn;
    public bool historyBtn => _historyBtn;
    public bool attackBtn => _attackBtn;
    public static InputAction NavigateUI => _navigateUI;

    public static float cursor_sensitivity = 2;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        _actionMap = ActionMaps.GamePlay;
        
        _navigateUI = _input.actions["Navigate"];
        _input.actions["Jump"].performed += OnJump;
    }

    private void Update()
    {
        
        _moveInput = _input.actions["Move"].ReadValue<Vector2>();
        _lookInput = _input.actions["Rotate"].ReadValue<Vector2>() * cursor_sensitivity;
        _nextBtn = _input.actions["NextMessage"].triggered;
        _historyBtn = _input.actions["History"].triggered;
        _attackBtn = _input.actions["Attack"].triggered;
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
}*/