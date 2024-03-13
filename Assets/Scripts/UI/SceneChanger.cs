using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject OptionsObj;
    public GameObject MenuObj;
    public GameObject Cube_Rotate;
    private UnityEngine.EventSystems.EventSystem myEventSystem;
    
    // Start is called before the first frame update
    private void Awake()
    {
        myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>(); 
    }

    public void LevelLoad()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ExitBtn()
    {
        Application.Quit();
    }

    public void OpenOptions() 
    {
        OptionsObj.SetActive(true);
        myEventSystem.SetSelectedGameObject(null);
        MenuObj.SetActive(false);
    }
    public void CloseOptions() 
    {
        MenuObj.SetActive(true);
        myEventSystem.SetSelectedGameObject(null);
        OptionsObj.SetActive(false);
    }


    public void RotateCubeHor(float z) => Cube_Rotate.transform.localEulerAngles = Cube_Rotate.transform.localEulerAngles + Vector3.forward * (z*10); //Cube_Rotate.transform.Rotate(Vector3.forward * (Cube_Rotate.transform.rotation.z - z)); //Cube_Rotate.transform.rotation = new Quaternion(0.0f, 0.0f, x*180, 1);//
    public void RotateCubeVert(float y) => Cube_Rotate.transform.localEulerAngles = Cube_Rotate.transform.localEulerAngles + Vector3.up * (y*10); //Cube_Rotate.transform.Rotate(Vector3.up * (Cube_Rotate.transform.rotation.y - y));//Cube_Rotate.transform.rotation = new Quaternion(0.0f, y*180,0.0f, 1);//
}
