using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip music;

    // Update is called once per frame
    void Update()
    {
        InputSys.Update();
    }
}
