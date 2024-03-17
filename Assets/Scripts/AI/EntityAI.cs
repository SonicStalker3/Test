using System;
using UnityEngine;

namespace AI
{
    public class EntityAI : MonoBehaviour
    {
        [SerializeField]
        private NavigationSystemSettings NavMesh;

        private int current_point = 0;

        private INavigatable _entity;
        private Vector3 RecalcuteTrajection()
        {
            float max_distance = float.MaxValue;
            int current_index = 0;
            for (var i = 0; i < NavMesh.points.Length; i++)
            {
                var point = NavMesh.points[i];
                if (Vector3.Distance(point, transform.position) < max_distance)
                {
                    max_distance = Vector3.Distance(point, transform.position);
                    //Debug.Log($"{point}, {transform.position} {max_distance}, {i}");
                    current_index = i;
                }
            }

            current_point = current_index;
            return NavMesh.points[current_index];
        }

        // Start is called before the first frame update
        void Start()
        {
            _entity = GetComponent<INavigatable>();
        }

        // Update is called once per frame
        void Update()
        {
            var dest = RecalcuteTrajection();
            if (Vector3.Distance(dest, transform.position) != 0) _entity.NavMove(dest);
            else current_point = NavMesh.indexPointsTo[current_point];
        }
    }
}
