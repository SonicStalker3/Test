using System;
using System.Collections;
using Interfaces.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody), typeof(CharacterController))]
    public class Player : StatsEntity, ISpeakable
    {
        [Header("Base Values")] public float speed = 6.0f;
        public float jumpForce = 8.0f;
        public float rotateSpeed = 0.8f;
        [Range(5f, 50f), SerializeField] private float gravity = 20.0f;
        [Range(0, 20f)] public float MaxAttackDistance = 5;
        [Header("DialogGameObjects"), Space] public GameObject DiagPanel;
        public GameObject HistoryPanel;
        public GameObject HistoryPanelView;
        private bool isControll = true;
        public bool isDialog;
        
        private AudioSource _voice;
        [SerializeField]
        private AudioClip[] dialogySounds;

        [SerializeField] private GameObject GameMenuPrefab;
        private GameMenu Menu;


        //private IControllable _controllable;
        //private InputSys _inputSys;
        PlayerInput _input;

        private Vector3 moveDirection = Vector3.zero;

        private CharacterController controller;
        private Transform playerCamera;
        private ParticleSystem _dustEffect;

        /*private PlayerInput _input;
        private Vector2 _moveInput;
        private Vector2 _lookInput;
        private bool jumpCond;*/
        private Camera _camera;
        [SerializeField] private bool isAttacking;
        public float attackRate = 10f;
        //[Range(1, 10)] [SerializeField] private int maxAttackRate = 5;

        private void Start()
        {
            _camera = Camera.main;
            health = MaxHealth;
            
            _input = GetComponent<PlayerInput>();
            InputSys.RegisterPlayerHandler(_input);
            
            _voice = GetComponent<AudioSource>();
            
            controller = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<Camera>().transform;
            //_inputSys = GetComponent<InputSys>();
            _dustEffect = GetComponentInChildren<ParticleSystem>();
            
            GameMenuPrefab = Resources.Load<GameObject>("Prefabs/UI/GameMenu");
            Menu = Instantiate(GameMenuPrefab, Vector3.zero, Quaternion.identity).GetComponent<GameMenu>();
            //_input._input.actions["Jump"].pre+= Jump;
        }

        void Update()
        {
            InputSys.Update();
            if (InputSys.MenuBtn)
            {
                Menu.ShoudOpen = !Menu.ShoudOpen;
                if(Menu.ShoudOpen) Menu.OpenMenu();
                Debug.Log(Menu.ShoudOpen);
            }

            isControll = !(!Menu.ShoudOpen && isDialog) ^ (Menu.ShoudOpen && !isDialog);
            
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
                if (InputSys.AttackBtn)
                {
                    StartCoroutine(nameof(AttackWithDelay));
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
            }
            DiagPanel.SetActive(isDialog);
            if (moveDirection == Vector3.zero) _dustEffect.Stop();
            else _dustEffect.Play();
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }

        private IEnumerator AttackWithDelay()
        {
            isAttacking = true;
            //animator.SetFloat("SpeedAttack",attackRate);
            yield return new WaitForSeconds(1 / attackRate);
            Attack();
            isAttacking = false;
        }

        void Attack()
        {
            Ray ray = _camera!.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.distance <= MaxAttackDistance)
                {
                    hit.collider.TryGetComponent(out Enemy entity);
                    /*Debug.Log($"{entity} {hit.collider.name}");*/
                    entity?.TakeDamage(baseDamage);
                }
            }
        }

        private void Move()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector2 lookDelta = InputSys.LookInput * (rotateSpeed); //* InputSys.CursorSensitivity
            /*if (playerModel)
        {
            playerModel.Rotate(0, _inputSys.lookInput.x * rotateSpeed, 0);
            playerCamera.Rotate(-_inputSys.lookInput.y * rotateSpeed, _inputSys.lookInput.x * rotateSpeed , 0);
        }*/
            transform.Rotate(0, lookDelta.x, 0);
            playerCamera.Rotate(-lookDelta.y, 0, 0);

            if (playerCamera.localRotation.eulerAngles.y != 0)
            {
                playerCamera.Rotate(lookDelta.y, 0, 0);
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
            isDialog = false;
        }

        protected override void OnDied()
        {
            throw new Exception("Вы умерли, поздравляю");
        }

        public void DialogySpeach()
        {
            if(dialogySounds != null) _voice.clip = dialogySounds[Random.Range(0,dialogySounds.Length-1)];
            _voice.Play();
        }
    }
}