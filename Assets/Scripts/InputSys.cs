using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum ActionMaps: byte
{
    GamePlay=0,
    Dialog
}


///Wrapper for easy interaction with the new Input System
public static class InputSys
{
    public static string IconsPath => _iconsPath;
    private static string _iconsPath = "Graphics/Buttons";
    
    //public static string IconsGroup = "";
    private static Vector2 _moveInput;
    private static Vector2 _lookInput;
    private static bool _jumpInput;
    private static bool _nextBtn;
    private static bool _historyBtn;
    private static bool _attackBtn;
    private static bool _interactBtn;
    
    private static InputAction _navigateUI;
    
    private static ActionMaps _actionMapEnum;
    private static InputActionMap _currentActionMap;
    private static string Type = "Keyboard&Mouse";
    

    private static readonly InputActionMap GameplayActionMap;
    private static readonly InputActionMap DialogActionMap;
    
    public static float cursor_sensitivity = 2;
    private static readonly InputActionAsset _gameplayControls;

    static InputSys()
    {
        _gameplayControls = Resources.Load<InputActionAsset>("GamePlayControlls");
        GameplayActionMap = _gameplayControls.FindActionMap("GamePlay");
        DialogActionMap = _gameplayControls.FindActionMap("Dialog");
        _navigateUI = _gameplayControls.FindActionMap("UI").FindAction("Navigate");

        GameplayActionMap.FindAction("Jump").performed += OnJump;
        GameplayActionMap.FindAction("Rotate").performed += TypeCheck;
        
        SwitchActionMap(ActionMaps.GamePlay);
    }

    private static void OnJump(InputAction.CallbackContext context)
    {
        _jumpInput = context.ReadValueAsButton();

    }

    private static void TypeCheck(InputAction.CallbackContext context)
    {
        foreach (var controlScheme in _gameplayControls.controlSchemes)
        {
            if (controlScheme.SupportsDevice(context.control.device))
                Type = controlScheme.name;
        }
        //Debug.Log(Type);
    }
    
    /// <summary>
    /// 
    /// </summary>
    public static void Update()
    {
        _moveInput = GameplayActionMap.FindAction("Move").ReadValue<Vector2>();
        _lookInput = GameplayActionMap.FindAction("Rotate").ReadValue<Vector2>();
        //_jumpInput = _gameplayActionMap.FindAction("Jump").triggered;
        _attackBtn = GameplayActionMap.FindAction("Attack").triggered;
        _interactBtn = GameplayActionMap.FindAction("Interact").triggered;
        
        _nextBtn = DialogActionMap.FindAction("NextMessage").triggered;
        _historyBtn = DialogActionMap.FindAction("History").triggered;

        
        /*_moveInput = _input.actions["Move"].ReadValue<Vector2>();
        _lookInput = _input.actions["Rotate"].ReadValue<Vector2>() * cursor_sensitivity;
        _nextBtn = _input.actions["NextMessage"].triggered;
        _historyBtn = _input.actions["History"].triggered;
        _attackBtn = _input.actions["Attack"].triggered;*/
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Возвращает словарь с ключом в ввиде пути("Type/Action") а в качестве значения ссылку на действие(Action)</returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionMap"></param>
    public static void SwitchActionMap(ActionMaps actionMap)
    {
        if (_currentActionMap != null)
        {
            _currentActionMap.Disable();
        }

        switch (actionMap)
        {
            case ActionMaps.GamePlay:
                _currentActionMap = GameplayActionMap;
                break;
            case ActionMaps.Dialog:
                _currentActionMap = DialogActionMap;
                break;
            default:
                _currentActionMap = GameplayActionMap;
                break;
        }

        _currentActionMap.Enable();
        _actionMapEnum = actionMap;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionMaps"></param>
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
                    _currentActionMap = GameplayActionMap;
                    break;
                case ActionMaps.Dialog:
                    _currentActionMap = DialogActionMap;
                    break;
                default:
                    _currentActionMap = GameplayActionMap;
                    break;
            }

            if (_currentActionMap != null) _currentActionMap.Enable();
            _actionMapEnum = map;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Action"></param>
    /// <param name="Type"></param>
    public static void Bind(InputAction Action)
    {
        GameplayActionMap.Disable();
        DialogActionMap.Disable();
     var rebind = new InputActionRebindingExtensions.RebindingOperation()
         .WithAction(Action)
         .WithBindingGroup(Type)//"Gamepad"
         .WithCancelingThrough("<Keyboard>/escape");
     rebind.Start();
     // inputAction.ApplyBindingOverride(bindingIndex, previousBinding);
     GameplayActionMap.Enable();
     DialogActionMap.Enable();
    }

    private static string ButtonName(string name)
    {
        return string.Join("_", "T", name,"Key_Dark");
    }
    public static Sprite ShowHelper(string action)
    {
        //Debug.Log(string.Join("/", IconsPath, Type ,ButtonName(GameplayActionMap.FindAction(action).GetBindingDisplayString())));
        return Resources.Load<Sprite>(string.Join("/", IconsPath, Type, ButtonName(GameplayActionMap.FindAction(action).GetBindingDisplayString())));
    }
    public static Vector2 MoveInput => _moveInput;
    public static Vector2 LookInput => _lookInput;
    public static bool JumpInput => _jumpInput;
    public static bool NextBtn => _nextBtn;
    public static bool HistoryBtn => _historyBtn;
    public static bool AttackBtn => _attackBtn;
    public static bool InteractBtn => _interactBtn;
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