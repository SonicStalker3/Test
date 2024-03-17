using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Entity, INavigatable
{
    private TextMesh nameLabel;

    private float _lastY;

    private int _currentPoint;
    // Start is called before the first frame update
    
    public void OnEnable()
    {
        IsDied += OnDied;
        //IsDied += DropItems;
    }
    
    void Start()
    {
        _health = MaxHealth;
        _lastY = transform.position.y;
        nameLabel = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        nameLabel.text = $"{E_name}: {Health}/{MaxHealth}";
    }
    
    public void NavMove(Vector3 dest)
    {
        var position = transform.position;
        dest.y = Mathf.Clamp(dest.y, position.y - 5, position.y + 5);
        //if (Math.Abs(dest.y - transform.position.y) > 45) Debug.Log("1");//dest.y = _lastY;
        //else _lastY = dest.y;
        if(Physics.Raycast(transform.position,(transform.position-dest).normalized, out var hit))
        {
            transform.position = Vector3.MoveTowards(transform.position, dest, 0.5f);
        }
        
    }

    private void DropItems()
    {
        
    }
}
