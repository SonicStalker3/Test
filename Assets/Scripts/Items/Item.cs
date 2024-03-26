using System;
using Interfaces;
using Scriptable;
using UnityEngine;


namespace Items
{
    ///UI Item Class
    public class Item: MonoBehaviour, IItem
    {
        
        [SerializeField]
        private ItemHandler _info;
        //[SerializeField]
        /*private int _count = 0;*/
        public ItemHandler Info => _info;
        /*public int Count => _count;*/

        /*private void Update()
        {
        }*/

        public bool Add(int count = 1)
        {
            return _info.Add(count);
        }
        

        public bool Remove(int count = 1)
        {
            return _info.Remove(count);
        }
        
    }
}