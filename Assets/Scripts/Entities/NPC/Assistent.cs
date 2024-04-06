using System.Collections;
using UnityEngine;

public class Assistent : NPC
{
    [Header("Assistent Properties")]
    public float searchRadius = 10f;
    public float attackRate = 2f;

    private bool isAttacking = false;
    void Start()
    {
        base.Init();
    }

    private void Update()
    {
        base.Update();
        FollowPlayer();
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        Enemy closestEnemy = GetClosestEnemy(colliders);

        if (closestEnemy != null && !isAttacking)
        {
            StartCoroutine(AttackWithDelay());
        }
        UpdateGUI();
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
        Enemy closestEnemy = GetClosestEnemy(Physics.OverlapSphere(transform.position, searchRadius));
        if (closestEnemy != null)
        {
            closestEnemy.TakeDamage(baseDamage);
        }
    }

    private Enemy GetClosestEnemy(Collider[] colliders)
    {
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            // Проверяем, является ли объект противником
            Enemy enemy = collider.GetComponent<Enemy>();
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