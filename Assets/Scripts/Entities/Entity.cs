using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour,IDamageble
{
    public string E_name;
    public int MaxHealth;
    public int MaxStamina;
    protected int _health = 100;
    protected int _stamina;
    protected int baseDamage = 10;
    public int Health => _health;

    public int Stamina => _stamina;

    public delegate void IsDiedEvent();
    public IsDiedEvent IsDied;

    public void Damage(int damage)
    {
        Debug.Log(Health);
        if (Health-damage > 0)
        {
            _health -= damage;
        }
        else
        {
            IsDied?.Invoke();
        }
    }

    public void OnDied()
    {
        Destroy(gameObject);
        Debug.Log($"Существо {E_name} умерло");
    }
}