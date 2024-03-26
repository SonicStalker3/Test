using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/*NPC Behaviour */
public class NPC : StatsEntity
{
    [Header("NPC Properties")] 
    public bool isFollow = true;
    public float followDistance = 5f; // Расстояние, на котором NPC следует за игроком
    private Player player;
    private NavMeshAgent navMeshAgent;
    
    [Header("Dialog System")]
    [SerializeField, Tooltip("Сюда нужно положить корневой файл диалога('DialogObject'(Instance of Scriptable Object))")]
    private QuestObject current_quest;

    //private InputSys _input;

    private Button DiagPanel;
    private GameObject HistoryPanel;
    private GameObject HistoryPanelView;
    private GameObject[] DiagList;
    private GameObject HistoryChoice;

    [Header("Dialog UI")]
    public Text NameField;
    public Text DialogField;

    private bool is_Triggert;
    //private bool prev_state = false;

    public delegate void EndDialogEvent();
    public EndDialogEvent OnDialogEnd;
    private Player _player;

    private void Start()
    {
    Init();
    }

    protected new void Init()
    {
        name_color = Color.green;
        name_color.a = 1;
        base.Init();
        isImmortal = true;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Debug.Log(player);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = followDistance;
        navMeshAgent.speed = moveSpeed;
    }

    protected void FollowPlayer()
    {
        if(isFollow) navMeshAgent.SetDestination(_player.transform.position);
    }
    private void Update()
    {
        FollowPlayer();
    }
}
