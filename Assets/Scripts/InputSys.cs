using UnityEngine;
using UnityEngine.InputSystem;

public enum ActionMaps: byte
{
    GamePlay=0,
    Dialog
}


/*
public static class InputManager
{
    private static Vector2 _moveInput;
    private static Vector2 _lookInput;
    private static bool _jumpInput;
    private static bool _nextBtn;
    private static bool _historyBtn;
    private static bool _attackBtn;

    private static InputActionMap _currentActionMap;

    private static InputActionMap _gameplayActionMap;
    private static InputActionMap _dialogActionMap;
    /*public enum ActionMaps: byte
    {
        GamePlay=0,
        Dialog
    }#1#

    static InputManager()
    {
        // Загрузите InputActionAsset из ресурсов
        var gameplayControls = Resources.Load<InputActionAsset>("GamePlayControlls");

        // Создайте экземпляры InputActionMap для каждого ActionMap
        _gameplayActionMap = gameplayControls.FindActionMap("GamePlay");
        _dialogActionMap = gameplayControls.FindActionMap("Dialog");

        // Установите обработчики для действий в Gameplay ActionMap
        _gameplayActionMap.FindAction("Move").performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _gameplayActionMap.FindAction("Look").performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _gameplayActionMap.FindAction("Jump").performed += ctx => _jumpInput = ctx.ReadValue<bool>();
        _gameplayActionMap.FindAction("Next").performed += ctx => _nextBtn = ctx.ReadValue<bool>();
        _gameplayActionMap.FindAction("History").performed += ctx => _historyBtn = ctx.ReadValue<bool>();
        _gameplayActionMap.FindAction("Attack").performed += ctx => _attackBtn = ctx.ReadValue<bool>();

        // Установите Gameplay как текущий ActionMap по умолчанию
        SwitchActionMap(ActionMaps.GamePlay);
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
    }

    public static Vector2 MoveInput => _moveInput;
    public static Vector2 LookInput => _lookInput;
    public static bool JumpInput => _jumpInput;
    public static bool NextBtn => _nextBtn;
    public static bool HistoryBtn => _historyBtn;
    public static bool AttackBtn => _attackBtn;
}
*/


public class InputSys: MonoBehaviour
{
    public PlayerInput _input;
    /*[SerializeField]
    private InputActionAsset _altInput;*/
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
}