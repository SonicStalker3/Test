using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : StatsEntity
{
    [SerializeField]
    private float DetectionRange = 5f;
    //private TextMeshProUGUI nameLabel;
    private int _currentPoint;
    //[SerializeField] private Camera mainCamera;

/*    public new void OnEnable()
    {
        base.OnEnable();
        IsDied += DropItems;
    }*/
    
    void Start()
    {
        Init();
    }

    protected new void Init() 
    {
        name_color = Color.red;
        name_color.a = 1;
        base.Init();
    }
    void Update()
    {
        UpdateGUI();
    }

    private void DropItems()
    {
        
    }
}
