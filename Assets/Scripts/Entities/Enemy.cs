using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StatsEntity
{
    [SerializeField]
    private float DetectionRange = 5f;
    private NavMeshAgent agent;
    //private TextMeshProUGUI nameLabel;
    private int _currentPoint;
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
}
