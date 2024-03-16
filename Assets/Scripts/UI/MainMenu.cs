using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsObj;
    public GameObject MenuObj;
    public GameObject Cube_Rotate;
    private EventSystem _eventSystem;

    private GameObject MainMenuSelected;
    private GameObject OptionsSelected;
    public GameObject CloseOptionBtn;

    private void FixedUpdate()
    {
        // if (true)
        // {
        // }
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>(); 
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
        MainMenuSelected = _eventSystem.currentSelectedGameObject;
        if (!OptionsSelected) OptionsSelected = _eventSystem.currentSelectedGameObject;
        else OptionsSelected = CloseOptionBtn;
        _eventSystem.SetSelectedGameObject(OptionsSelected);
        MenuObj.SetActive(false);
    }
    public void CloseOptions() 
    {
        MenuObj.SetActive(true);
        _eventSystem.SetSelectedGameObject(MainMenuSelected);
        OptionsObj.SetActive(false);
    }


    public void RotateCubeHor(float z) => Cube_Rotate.transform.localEulerAngles = Cube_Rotate.transform.localEulerAngles + Vector3.forward * (z*10); //Cube_Rotate.transform.Rotate(Vector3.forward * (Cube_Rotate.transform.rotation.z - z)); //Cube_Rotate.transform.rotation = new Quaternion(0.0f, 0.0f, x*180, 1);//
    public void RotateCubeVert(float y) => Cube_Rotate.transform.localEulerAngles = Cube_Rotate.transform.localEulerAngles + Vector3.up * (y*10); //Cube_Rotate.transform.Rotate(Vector3.up * (Cube_Rotate.transform.rotation.y - y));//Cube_Rotate.transform.rotation = new Quaternion(0.0f, y*180,0.0f, 1);//
}
