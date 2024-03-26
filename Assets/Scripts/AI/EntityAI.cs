using System.Collections;
using Scriptable.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMesh))]
public class EntityAI : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Enemy Enemies;
    [SerializeField]
    private NavigationSystemSettings NavMesh;
    [SerializeField]
    private float visionRange = 10f;
    [SerializeField]
    private float DetectionRange = 5f;
    [SerializeField]
    private float AttackRange = 1.5f;

    private int current_point = 0;
    private NavMeshAgent agent;
    private bool isAttacking;
    [SerializeField] private int attackRate;

    void Start()
    {
        Enemies = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        // Инициализация ссылки на игрока, если она не была установлена через редактор
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
    /*private IEnumerator AttackWithDelay()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1 / attackRate);
        gameObject.GetComponent<Enemy>()?.Attack();
        isAttacking = false;
    }*/

     void Update()
     {
            RaycastHit hit;
            if (CanSeePlayer(out hit))
            {
                if (hit.distance <= AttackRange)
                {
                    
                }
                agent.SetDestination(player.position);
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
    
     private bool CanSeePlayer(out RaycastHit hit)
     {
         Vector3 directionToPlayer = player.position - transform.position;
         if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionRange))
         {
             return hit.transform == player;
         }
         return false;
     }

     private bool IsEnemyInRange(Vector3 point)
     {
         Collider[] enemies = Physics.OverlapSphere(point, DetectionRange);
         foreach (var enemy in enemies)
         {
             if (enemy.CompareTag("Enemy") && enemy.gameObject != this.gameObject)
             {
                 return true;
             }
         }
         return false;
     }
     /*private bool IsPlayer(Vector3 point)
     {
         Collider[] enemies = Physics.OverlapSphere(point, DetectionRange);
         foreach (var enemy in enemies)
         {
             if (enemy.TryGetComponent(out Player entity))
             {
                 return true;
             }
         }
         return false;
     }*/

     private int FindNextSafePoint()
     {
         int nextPointIndex = (current_point + 1) % NavMesh.points.Length;
         if (IsEnemyInRange(NavMesh.points[nextPointIndex]))
         {
             nextPointIndex = (nextPointIndex + 1) % NavMesh.points.Length;
         }
         return nextPointIndex;
     }

     private void Patrol()
     {
         Vector3 nextPoint = NavMesh.points[current_point];
         //Debug.Log(Vector3.Distance(transform.position, nextPoint));
         //Debug.Log(agent.stoppingDistance);
         if (Vector3.Distance(transform.position, nextPoint) < 1) //agent.stoppingDistance
         {
             //current_point = FindNextSafePoint();
             current_point = NavMesh.indexPointsTo[current_point];
             //Debug.Log("patrool");
         }
         else
         {
             agent.SetDestination(nextPoint);
         }
     }

}
