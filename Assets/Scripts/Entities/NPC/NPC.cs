using Interfaces.Entities;
using Scriptable.Dialog;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entities.NPC
{
    /// <summary>
    ///NPC Behaviour
    /// </summary>
    public class NPC : StatsEntity, ISpeakable
    {
        [Header("NPC Properties")] 
        public bool isFollow = true;
        public float followDistance = 5f; // Расстояние, на котором NPC следует за игроком
    
        private NavMeshAgent _navMeshAgent;
        [Range(1,100)]
        public float triggerRadius;
        private bool _isTriggert;
        [FormerlySerializedAs("IteractUI")] [SerializeField]
        private GameObject iteractUI;
    
        [FormerlySerializedAs("current_quest")]
        [Header("Dialog System")]
        
        //На будущее
        [SerializeField, Tooltip("Сюда нужно положить корневой файл диалога('QuestObject'(Instance of Scriptable Object))")]
        private QuestObject currentQuest;
        
        [SerializeField, Tooltip("Сюда нужно положить корневой файл диалога('DialogObject'(Instance of Scriptable Object))")]
        private DialogObject dialog;

        //private InputSys _input;

        private DialogManager _dialogyScript;

        private AudioSource _voice;
        [SerializeField]
        private AudioClip[] dialogySounds;

        /*[Header("Dialog UI")]
    public Text NameField;
    public Text DialogField;*/


        //private bool prev_state = false;

        public delegate void EndDialogEvent();
        public EndDialogEvent OnDialogEnd;
        private Player _player;

        private void Start()
        {
            Init();
            _voice = GetComponent<AudioSource>();
            NpcManager.RegisterNpc(this, out _dialogyScript);
        }

        protected new void Init()
        {
            name_color = Color.green;
            name_color.a = 1;
            base.Init();
            //NameFormat = "{0}";
            isImmortal = true;
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            Debug.Log(_player); 
            if (TryGetComponent(out _navMeshAgent))
            {
                //navMeshAgent.stoppingDistance = ;
                _navMeshAgent.speed = moveSpeed;
            }
        }

        protected void FollowPlayer()
        {
            if (isFollow)
            {
                var velocity = _navMeshAgent.velocity;
                bool isWalking = velocity.magnitude > 0.1f;
                animator.SetFloat("SpeedHorizontal",velocity.x);
                animator.SetFloat("SpeedVertical",velocity.y);
                var pT = _player.transform;
                if (Vector3.Distance(transform.position, pT.position) > followDistance)
                {
                    var position = pT.position;
                    _navMeshAgent.SetDestination(
                        position - (position - transform.position).normalized * followDistance);
                }

                if (animator)
                {
                    if (animator.GetBool("isWalk") != isWalking)
                    {
                        Debug.Log($"{animator.GetBool("isWalk")} {isWalking}");
                        animator.SetBool("isWalk", isWalking);
                    }
                }
                //navMeshAgent.SetDestination(_player.transform.position);
            }
        }
        protected void Update()
        {
            FollowPlayer();
            CheckIteract();
            UpdateGUI();
            //Debug.Log(string.Format(NameFormat,new object[]{1,2,3}));
        }

        private void CheckIteract()
        {
            bool isInteractable = Vector3.Distance(_player.transform.position, transform.position) <  triggerRadius;
            //IteractUI.SetActive(!is_Triggert);
            if (Vector3.Distance(_player.transform.position, transform.position) <  triggerRadius)
            {
                iteractUI.GetComponentInChildren<Image>().sprite = InputSys.ShowHelper("Interact");
                iteractUI.SetActive(!_isTriggert);
                if (InputSys.InteractBtn && !_isTriggert)
                {
                    Debug.Log("Start Dialog");
                    _dialogyScript.OpenDialog(_player, dialog, animator,2);
                    _dialogyScript.AddSpeaker(0, _player);
                    _dialogyScript.AddSpeaker(1,this);
                }
                //Debug.Log("Can Iteract");
            }
            else
            {
                iteractUI.SetActive(false);
            }
        }

        public void DialogySpeach()
        {
            if(dialogySounds != null) _voice.clip = dialogySounds[Random.Range(0,dialogySounds.Length-1)];
            _voice.Play();
        }

        new void UpdateGUI()
        {
            NameFormat = "{0}";
            base.UpdateGUI();
        }
    }
}
