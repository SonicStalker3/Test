using System;
using Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : GameMenu
    {
        [SerializeField]
        private PlayerInput _input;
        
        private void Awake()
        {
            ShoudOpen = true;
            //_input = _eventSystem.gameObject.GetComponent<PlayerInput>();
            InputSys.RegisterUIHandler(_input);
        }

        [SerializeField] private GameObject secretChoiseLevelPanel;
        public new void StartBtn()
        {
            SceneManager.LoadScene(1);
        }
    
        public new void ExitBtn()
        {
            Application.Quit();
        }

        public void LevelChoicePanel()
        {
            secretChoiseLevelPanel.SetActive(!secretChoiseLevelPanel.activeSelf);
        }
        public void LevelChoice(int level)
        {
            if (level <= 0) throw new ArgumentOutOfRangeException(nameof(level));
            SceneManager.LoadScene(level);
        }
    }
}
