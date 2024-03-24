using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : GameMenu
{
    public new void StartBtn()
    {
        SceneManager.LoadScene(1);
    }
    
    public new void ExitBtn()
    {
        Application.Quit();
    }
}
