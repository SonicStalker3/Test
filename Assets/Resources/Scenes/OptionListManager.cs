using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionListManager : MonoBehaviour
{
    public Button[] menuButtons;
    // public GameObject[] menuPanels;

    void Start()
    {
        menuButtons = GetComponentsInChildren<Button>();
        InputSys.NavigateUI.canceled += OnNavigate;
    }
    
    /*public void OnSelect(BaseEventData eventData)
    {
        GameObject selectedObject = eventData.selectedObject;
        
        if (menuButtons.Any(button => button.gameObject == selectedObject))
        {
            selectedObject.GetComponent<Button>().onClick.Invoke();
        }
    }*/
    
    void OnNavigate(InputAction.CallbackContext context)
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        
        if (menuButtons.Any(button => button.gameObject == selectedObject))
        {
            selectedObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    /*
    public void OptionClicked(int option)
    {
        for (int i = 0; i < menuPanels.Length; i++)
        {
            var obj = menuPanels[i]; 
            obj.SetActive(i == option);
        }
    }*/
    
}
