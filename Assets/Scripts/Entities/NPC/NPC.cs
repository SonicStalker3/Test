using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/*NPC Behaviour */
public class NPC : StatsEntity
{
    [Header("NPC Properties")] 
    public bool isFollow = true;
    public float followDistance = 5f; // Расстояние, на котором NPC следует за игроком
    //protected Player _player;
    private NavMeshAgent navMeshAgent;
    [Range(1,100)]
    public float triggerRadius;
    private bool is_Triggert;
    [SerializeField]
    private GameObject IteractUI;
    
    [Header("Dialog System")]
    [SerializeField, Tooltip("Сюда нужно положить корневой файл диалога('DialogObject'(Instance of Scriptable Object))")]
    private QuestObject current_quest;

    //private InputSys _input;

    private DialogyScript _dialogyScript;

    /*[Header("Dialog UI")]
    public Text NameField;
    public Text DialogField;*/


    //private bool prev_state = false;

    public delegate void EndDialogEvent();
    public EndDialogEvent OnDialogEnd;
    private Player _player;

    private void Awake()
    {
    Init();
    NPCManager.RegisterNPC(this);
    }

    protected new void Init()
    {
        name_color = Color.green;
        name_color.a = 1;
        base.Init();
        isImmortal = true;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Debug.Log(_player);
        navMeshAgent = GetComponent<NavMeshAgent>();
        //navMeshAgent.stoppingDistance = ;
        navMeshAgent.speed = moveSpeed;
        
    }

    protected void FollowPlayer()
    {
        if (isFollow)
        {
            var velocity = navMeshAgent.velocity;
            bool isWalking = velocity.magnitude > 0.1f;
            animator.SetFloat("SpeedHorizontal",velocity.x);
            animator.SetFloat("SpeedVertical",velocity.y);
            var pT = _player.transform;
            if (Vector3.Distance(transform.position, pT.position) > followDistance)
                navMeshAgent.SetDestination(
                    pT.position - (pT.position - transform.position).normalized * followDistance);
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
    }

    private void CheckIteract()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <  triggerRadius)
        {
            IteractUI.GetComponentInChildren<Image>().sprite = InputSys.ShowHelper("Interact");
            IteractUI.SetActive(!is_Triggert);
            if (InputSys.InteractBtn && !is_Triggert)
            {
                Debug.Log("Start Dialog");
            }
            //Debug.Log("Can Iteract");
        }
    }
}
