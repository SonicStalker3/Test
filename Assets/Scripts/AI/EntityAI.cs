using UnityEngine;
using UnityEngine.AI;

public class EntityAI : MonoBehaviour
{
    [SerializeField]
    private Transform player; // Ссылка на объект игрока
    [SerializeField]
    private NavigationSystemSettings NavMesh;
    [SerializeField]
    private float visionRange = 10f; // Дальность видимости
    [SerializeField]
    private float enemyDetectionRange = 5f; // Дальность обнаружения врага

    private int current_point = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Инициализация ссылки на игрока, если она не была установлена через редактор
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            FollowPlayer();
        }
        else
        {
            if (IsEnemyInRange(NavMesh.points[current_point]))
            {
                current_point = FindNextSafePoint();
            }
            Patrol();
        }
    }

    private bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - transform.position;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionRange))
        {
            return hit.transform == player;
        }
        return false;
    }

    private bool IsEnemyInRange(Vector3 point)
    {
        // Проверяем, есть ли враги в заданном радиусе от точки
        Collider[] enemies = Physics.OverlapSphere(point, enemyDetectionRange);
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }

    private int FindNextSafePoint()
    {
        int nextPointIndex = (current_point + 1) % NavMesh.points.Length;
        while (IsEnemyInRange(NavMesh.points[nextPointIndex]))
        {
            nextPointIndex = (nextPointIndex + 1) % NavMesh.points.Length;
        }
        return nextPointIndex;
    }

    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Patrol()
    {
        Vector3 nextPoint = NavMesh.points[NavMesh.indexPointsTo[current_point]];
        if (Vector3.Distance(transform.position, nextPoint) < agent.stoppingDistance)
        {
            current_point = NavMesh.indexPointsTo[current_point];
        }
        else
        {
            agent.SetDestination(nextPoint);
        }
    }
}
