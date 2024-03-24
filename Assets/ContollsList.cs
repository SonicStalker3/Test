using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContollsList : MonoBehaviour
{
    public Transform buttonParent; // Родительский объект для кнопок
    public GameObject buttonPrefab; // Префаб кнопки
    [SerializeField]
    private Button[] GroupButtonList;
    // Update is called once per frame
    private void Start()
    {
        buttonPrefab  = Resources.Load<GameObject>("Prefabs/UI/Button");
        foreach (var bind in InputSys.ActionsList())
        {
            string[] parts = bind.Key.Split('/');
            string Type = parts[0];
            string Value = parts[1];

            // Загрузка префаба
            GameObject buttonObject = Resources.Load<GameObject>("Prefabs/UI/Bind_Chain");

            // Создание кнопки
            GameObject newButton = Instantiate(buttonObject, buttonParent);

            // Настройка текста кнопки
            Text buttonText = newButton.GetComponentInChildren<Text>();
            buttonText.text = Value;

            // Настройка действия при нажатии кнопки
            Button buttonComponent = newButton.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => {InputSys.Bind(bind.Value); // Вызов действия, связанного с кнопкой
            });
        }
    }
}
