using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionListManager : MonoBehaviour
{
    public Button[] menuButtons;

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
        // Получаем текущий выбранный объект
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        // Проверяем, является ли выбранный объект одной из кнопок в массиве menuButtons
        if (menuButtons.Any(button => button.gameObject == selectedObject))
        {
            // Если да, вызываем событие onClick
            selectedObject.GetComponent<Button>().onClick.Invoke();
        }
    }
}
