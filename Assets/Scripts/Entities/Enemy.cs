using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : Entity
{
    private TextMesh nameLabel;
    // Start is called before the first frame update
    
    public void OnEnable()
    {
        IsDied += OnDied;
        IsDied += DropItems;
    }
    
    void Start()
    {
        _health = MaxHealth;
        nameLabel = GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        nameLabel.text = $"{E_name}: {Health}/{MaxHealth}";
    }

    private void DropItems()
    {
        
    }
}
