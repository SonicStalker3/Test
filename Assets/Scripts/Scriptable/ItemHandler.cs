using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptable
{
    ///Data Item class
    [CreateAssetMenu(menuName = "Items/Item", order=-1)]
    public class ItemHandler : ScriptableObject
    {
        [SerializeField]
        [Tooltip("Иконка предмета в инвенторе")]
        public Sprite Icon;
        [Tooltip("Имя предмета в инвенторе")]
        
        public string ItemName;
        [Tooltip("Описание предмета в инвенторе")]
        [Multiline(15)]
        public string description;
        [Tooltip("Максимальное количество обьектов в стаке")]
        [Range(1,ushort.MaxValue)]
        public ushort maxStack = 10;
        public int Count => _count;
        
        private int _count = 0;
    
        public GameObject viewModel;
        //public IItemEffect a;

        public bool Add(int count = 1)
        {
            if (count < 0) throw new ArgumentException($"Функция '{nameof(Add)}' не поддерживает негативное значение для параметра 'count'. 'count' не может быть равен {count}", nameof(count));
            _count += count;
            if (Count + count > maxStack) return false;
            _count -= count;
            return true;

        }
        
        public bool Remove(int count = 1)
        {
            if (count < 0) throw new ArgumentException($"Функция '{nameof(Remove)}' не поддерживает негативное значение для параметра 'count'. 'count' не может быть равен {count}", nameof(count));
            if (Count - count < 0) return false;
            _count -= count;
            return true;

        }
        private void OnValidate()
        {
            //maxStack = (ushort)Mathf.Clamp(maxStack, 1, short.MaxValue);
        }
    }
}
