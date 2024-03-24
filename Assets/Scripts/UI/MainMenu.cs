using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsObj;
    public GameObject MenuObj;
    public GameObject Cube_Rotate;
    private EventSystem _eventSystem;
    public GameObject[] OptionsList;

    [SerializeField]
    private GameObject MainMenuSelected;
    [SerializeField]
    private GameObject OptionsSelected;
    public GameObject CloseOptionBtn;
    private void Awake()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        
    }

    private void Start()
    {
        var panel = OptionsObj.transform.GetChild(1);//GameObject.Find("OptionPanels");
        Debug.Log(panel.name);
        int children = panel.transform.childCount;
        OptionsList = new GameObject[children];
        for (int i = 0; i < children; ++i)
        {
            OptionsList[i] = panel.transform.GetChild(i).gameObject;
        }
    }

    public void StartBtn()
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
        OptionsSelected = !OptionsSelected ? _eventSystem.currentSelectedGameObject : CloseOptionBtn;
        _eventSystem.SetSelectedGameObject(OptionsSelected);
        MenuObj.SetActive(false);
    }
    
    public void CloseOptions() 
    {
        MenuObj.SetActive(true);
        _eventSystem.SetSelectedGameObject(MainMenuSelected);
        OptionsObj.SetActive(false);
    }

    public void OpenOption(int currentOption)
    {
        for (int i = 0; i < OptionsList.Length; ++i)
        {
            OptionsList[i].SetActive(i == currentOption);
        }

    }


    public void RotateCubeHor(float z) => Cube_Rotate.transform.localEulerAngles = Cube_Rotate.transform.localEulerAngles + Vector3.forward * (z*10); //Cube_Rotate.transform.Rotate(Vector3.forward * (Cube_Rotate.transform.rotation.z - z)); //Cube_Rotate.transform.rotation = new Quaternion(0.0f, 0.0f, x*180, 1);//
    public void RotateCubeVert(float y) => Cube_Rotate.transform.localEulerAngles = Cube_Rotate.transform.localEulerAngles + Vector3.up * (y*10); //Cube_Rotate.transform.Rotate(Vector3.up * (Cube_Rotate.transform.rotation.y - y));//Cube_Rotate.transform.rotation = new Quaternion(0.0f, y*180,0.0f, 1);//
}
