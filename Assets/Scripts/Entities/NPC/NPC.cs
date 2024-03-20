using UnityEngine;
using UnityEngine.AI;

public class NPC : Entity
{
    [Header("NPC Properties")]
    public float followDistance = 5f; // Расстояние, на котором NPC следует за игроком
    private Transform player;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
    Init();
    }

    protected void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = followDistance;
        navMeshAgent.speed = moveSpeed;
    }

    protected void FollowPlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }
    private void Update()
    {
        FollowPlayer();
    }
}
