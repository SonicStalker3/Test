using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entities
{
    /// <summary>
    /// Base Enemy behavior
    /// </summary>
    public class Enemy : StatsEntity
    {
        [SerializeField]
        private float DetectionRange = 5f;
        public float searchRadius = 10f;
        public float attackRate = 2f;
    
        private NavMeshAgent agent;
        //private TextMeshProUGUI nameLabel;
        private int _currentPoint;
        private bool isAttacking = false;

        private Player closestEnemy;
        //[SerializeField] private Camera mainCamera;

/*    public new void OnEnable()
    {
        base.OnEnable();
        IsDied += DropItems;
    }*/
    
        void Awake()
        {
            Init();
        }

        protected new void Init() 
        {
            name_color = Color.red;
            name_color.a = 1;
            base.Init();
            agent = GetComponent<NavMeshAgent>();
            animator.SetFloat("Speed",moveSpeed/5);
        }
        void Update()
        {
            UpdateGUI();
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
            closestEnemy = GetClosestEnemy(colliders);
            if (closestEnemy != null && !isAttacking)
            {
                StartCoroutine(AttackWithDelay());
            }
        }

        public void Move(Vector3 dest)
        {
            agent.SetDestination(dest);
            if (animator)
            {
                bool isWalking = agent.velocity.magnitude > 0.1f;
                //Debug.Log(agent.velocity.magnitude);
                animator.SetBool("isWalk", isWalking);
            }
        
        }

        private void DropItems()
        {
        
        }
    
        private IEnumerator AttackWithDelay()
        {
            isAttacking = true;
            animator.SetFloat("AttackType", Random.Range(1,4));
            animator.SetFloat("Blend", 0.5f);
            //animator.SetFloat("SpeedAttack",attackRate);
            yield return new WaitForSeconds(1 / attackRate);
            AttackEnemy();
            isAttacking = false;
            animator.SetFloat("AttackType", 0);
            animator.SetFloat("Blend", 0);
        }
    
        private void AttackEnemy()
        {
            closestEnemy = GetClosestEnemy(Physics.OverlapSphere(transform.position, searchRadius));
            if (closestEnemy != null)
            {
                closestEnemy.TakeDamage(baseDamage);
            }
        }
    
        private Player GetClosestEnemy(Collider[] colliders)
        {
            closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (var collider in colliders)
            {
                // Проверяем, является ли объект противником
                Player enemy = collider.GetComponent<Player>();
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }

            //Debug.Log(closestEnemy);
            return closestEnemy;
        }
    }
}
