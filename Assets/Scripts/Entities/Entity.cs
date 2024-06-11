using Interfaces;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageble
    {
        [Header("Entity Properties")]
        public string E_name;
        public int MaxHealth;
        public int MaxStamina;

        protected int health = 100;

        //[Range(5f,35f)]
        public int moveSpeed = 5;
        protected bool isImmortal;
        public int Health => health;

        protected delegate void IsDiedEvent();

        protected IsDiedEvent IsDied;
        public virtual void OnEnable()
        {
            IsDied += OnDied;
        }

        public virtual void OnDisable()
        {
            IsDied -= OnDied;
        }

        public void TakeDamage(int damage)
        {
            // Debug.Log("?D");
            if (!isImmortal) 
            {
//            Debug.Log(Health);
                if (Health - damage >= 0)
                {
                    health -= damage;
                }
                else
                {
                    IsDied?.Invoke();
                }
            }
        }

        protected virtual void Init() 
        {
            health = MaxHealth;
        }

        protected virtual void OnDied()
        {
            Destroy(gameObject);
            Debug.Log($"Существо {E_name} умерло");
        }
    }
}