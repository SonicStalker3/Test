using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateClins : MonoBehaviour
{
    public Vector3 vertex;
    public Vector3 offset;
    public float Multiplyer = 2.5f;
    private GameObject Prefab;
    private int[] pascal;

    // Start is called before the first frame update
    void Start()
    {
        Prefab = Resources.Load<GameObject>("Prefab");
        for (int i = -1; i <= 1; i++) 
        {
            Instantiate(Prefab, new Vector3(vertex.x + (offset.x*i) + (i*Multiplyer), vertex.y+(offset.y * i), vertex.z + (i*i) * offset.z), Quaternion.identity);
        }
        int n = 5;

        for (int y = 0; y < n; y++)
        {
            int c = 1;
            for (int x = 0; x <= y; x++)
            {
                Debug.Log($"   {c:D} ");
                c = c * (y - x) / (x + 1);
            }
            Debug.Log("\n");
            Debug.Log("\n");
        }
        Debug.Log("\n");
    }
}
