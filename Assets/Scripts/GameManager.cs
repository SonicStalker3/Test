using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip music;
    // Start is called before the first frame update
    void Start()
    {
     //AudioSource   
    }

    // Update is called once per frame
    void Update()
    {
        InputSys.Update();
    }
}
