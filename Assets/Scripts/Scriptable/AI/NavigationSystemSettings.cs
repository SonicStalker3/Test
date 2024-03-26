using System;
using UnityEngine;

namespace Scriptable.AI
{
    /// Patrol Move Trajections
    [CreateAssetMenu(menuName = "Navigation/NavSysSettings", order=-1)]
    public class NavigationSystemSettings : ScriptableObject
    {
        public Vector3[] points;
        public int[] indexPointsTo;
    
        public void OnValidate()
        {
            for (int i = 0; i < indexPointsTo.Length; i++)
            {
                if (indexPointsTo[i] < 0 || indexPointsTo[i] > points.Length-1)
                {
                    indexPointsTo[i] = 0;
                }
            }
            if (indexPointsTo != null && points.Length != indexPointsTo.Length)
            {
                Array.Resize(ref indexPointsTo,points.Length);
            }
            else if(points.Length == 1)
            {
                indexPointsTo = new int[points.Length];
            }
        }
    }
}
