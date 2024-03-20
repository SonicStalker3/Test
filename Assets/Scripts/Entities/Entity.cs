using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour,IDamageble
{
    [Header("Entity Properties")]
    public string E_name;
    public int MaxHealth;
    public int MaxStamina;
    
    protected int health = 100;
    protected int stamina;
    //[Range(5f,35f)]
    public int moveSpeed = 5;
    public int baseDamage = 10;
    
    public int Health => health;

    public int Stamina => stamina;

    protected delegate void IsDiedEvent();

    protected IsDiedEvent IsDied;

    public void TakeDamage(int damage)
    {
        Debug.Log(Health);
        if (Health-damage > 0)
        {
            health -= damage;
        }
        else
        {
            IsDied?.Invoke();
        }
    }

    protected void OnDied()
    {
        Destroy(gameObject);
        Debug.Log($"Существо {E_name} умерло");
    }
}