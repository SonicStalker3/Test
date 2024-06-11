using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameMenu : MonoBehaviour
{
    public GameObject OptionsObj;
    public GameObject MenuObj;
    private EventSystem _eventSystem;
    public GameObject[] OptionsList;

    [SerializeField]
    private GameObject MenuSelected;
    [SerializeField]
    private GameObject OptionsSelected;
    public GameObject CloseOptionBtn;

    public bool ShoudOpen = false;
    // Start is called before the first frame update
    private void Awake()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        var panel = OptionsObj.transform.GetChild(1);//GameObject.Find("OptionPanels");
        int children = panel.transform.childCount;
        OptionsList = new GameObject[children];
        for (int i = 0; i < children; ++i)
        {
            OptionsList[i] = panel.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        gameObject.SetActive(ShoudOpen);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
        _eventSystem.SetSelectedGameObject(MenuSelected);
    }
    
    public void StartBtn()
    {
        ShoudOpen = false;
        gameObject.SetActive(ShoudOpen);
    }
    
    public void ExitBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenOptions() 
    {
        OptionsObj.SetActive(true);
        MenuSelected = _eventSystem.currentSelectedGameObject;
        OptionsSelected = !OptionsSelected ? _eventSystem.currentSelectedGameObject : CloseOptionBtn;
        _eventSystem.SetSelectedGameObject(OptionsSelected);
        MenuObj.SetActive(false);
    }
    
    public void CloseOptions() 
    {
        MenuObj.SetActive(true);
        _eventSystem.SetSelectedGameObject(MenuSelected);
        OptionsObj.SetActive(false);
    }

    public void OpenOption(int currentOption)
    {
        for (int i = 0; i < OptionsList.Length; ++i)
        {
            OptionsList[i].SetActive(i == currentOption);
        }
    }

    public void OnSensitivityChanged(float x)
    {
        //InputSys.cursor_sensitivity = x;
    }
    
    public void OnMusicChanged(float x)
    {
        //InputSys.cursor_sensitivity = x;
    }
    public void OnGraphicsChanged(float x)
    {
        InputSys.cursor_sensitivity = x;
    }
}
