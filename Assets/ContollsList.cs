/*using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContollsList : MonoBehaviour
{
    private Dictionary<string, InputAction> controlSchemes;

    private Transform buttonsRoot;
    private GameObject bindChain;
    private GameObject buttonPrefab;

    private void Start()
    {
        bindChain = Resources.Load<GameObject>("Prefabs/UI/Controls/Bind_Chain");
        buttonPrefab = Resources.Load<GameObject>("Prefabs/UI/Controls/Button");

        buttonsRoot = transform.Find("Buttons");

        controlSchemes = InputSys.ActionsList();

        CreateGroups();

        CreateActionButtons();
    }

    private void CreateGroups()
    {
        var groups = new List<string>();

        foreach (var action in controlSchemes)
        {
            if (!groups.Contains(action.Key.Split('/')[0]))
            {
                groups.Add(action.Key.Split('/')[0]);
            }
        }

        foreach (var group in groups)
        {
            var label = Instantiate(buttonPrefab, buttonsRoot);
            label.GetComponentInChildren<Text>().text = group;
            label.GetComponent<Button>().interactable = false;
        }
    }

    private void CreateActionButtons()
    {
        var actions = new List<InputAction>(controlSchemes.Values);

        foreach (var action in actions)
        {
            var actionType = action.type;
            if (ActParse(action) is GameObject buttonObj)
            {
                //var button = buttonObj.transform.GetChild(0).GetComponent<Button>();
                var button = buttonObj.GetComponent<Button>();
                button.onClick.AddListener(() => InputSys.Bind(action, "Keyboard"));

                var label = Instantiate(buttonPrefab, buttonsRoot);
                label.transform.SetParent(actionType == InputActionType.Button ? buttonObj.transform : buttonObj.transform.parent);

                label.GetComponentInChildren<Text>().text = action.name;
            }
        }
    }

    private GameObject ActParse(InputAction action)
    {
        if (action.type == InputActionType.Button)
        {
            var bind = Instantiate(bindChain, buttonsRoot);

            var nameText = bind.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            nameText.text = action.name;

            var bindBtnText = bind.transform.Find("Bind").Find("Text").GetComponent<TextMeshProUGUI>();
            bindBtnText.text = action.bindings[0].effectivePath;

            var bindBtn = bind.transform.Find("Bind").GetComponent<Button>();
            bindBtn.onClick.AddListener(() => InputSys.Bind(action));

            return bind.gameObject;
        }

        return null;
    }
}*/

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class ContollsList : MonoBehaviour
{
    [SerializeField]
    private Button Tab;
    private GameObject _bindChainPrefab;
    private GameObject _buttonPrefab;
    private GameObject _labelPrefab;
    [SerializeField]
    private string[] ignoreLayers;

    private Transform _groupContainer;
    private Transform _buttonContainer;

    private void Awake()
    {
        _bindChainPrefab = Resources.Load<GameObject>("Prefabs/UI/Controls/Bind_Chain");
        _buttonPrefab = Resources.Load<GameObject>("Prefabs/UI/Controls/Button");
        _labelPrefab = Resources.Load<GameObject>("Prefabs/UI/Controls/Label");
        // Create a new label as a child of the GameObject this script is attached to
        /*GameObject labelObj = Instantiate(_labelPrefab,transform);
        TextMeshProUGUI label = labelObj.GetComponent<TextMeshProUGUI>();
        label.text = "GamePlay"; // Set the label text to "GamePlay"
        label.fontSize = 18;

        // Create a new VerticalLayoutGroup as a child of the GameObject this script is attached to
        GameObject layoutObj = Instantiate(new GameObject("Layout"),transform);
        layoutObj.AddComponent<VerticalLayoutGroup>();
        VerticalLayoutGroup layout = layoutObj.GetComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 5, 5);
        layout.spacing = 5;
        layout.childControlHeight = false;*/

        // Get the InputSys.ActionsList dictionary
        Dictionary<string, InputAction> actionsList = InputSys.ActionsList();

        int c = 0;
        string currentKey = null;
        
        GameObject labelObj = null;
        TextMeshProUGUI label =null;
        GameObject layoutObj = null;
        VerticalLayoutGroup layout = null;
        var contentRect = transform.GetComponent<RectTransform>();
        float contentSize = 0;
        float size = 0;
        // Loop through the dictionary and add a button for each key-value pair
        foreach (KeyValuePair<string, InputAction> action in actionsList)
        {
            string[] key = action.Key.Split('/');
            Debug.Log(key[0]);
            if (ignoreLayers.Any(n => key[0] != n))
            {
                if (currentKey != key[0])
                {
                    contentSize += size + 20;
                    c = 0;
                    labelObj = Instantiate(_labelPrefab,transform);
                    label = labelObj.GetComponent<TextMeshProUGUI>();
                    label.text = action.Key; // Set the label text to "GamePlay"
                    label.fontSize = 18;

                    // Create a new VerticalLayoutGroup as a child of the GameObject this script is attached to
                    layoutObj = Instantiate(new GameObject("Layout"),transform);
                    layoutObj.AddComponent<VerticalLayoutGroup>();
                
                    layout = layoutObj.GetComponent<VerticalLayoutGroup>();
                    layout.padding = new RectOffset(10, 10, 10, 10);
                    layout.spacing = 5;
                    layout.childControlHeight = true;
                
                    currentKey = key[0];
                }
                // Load the button prefab from the Resources folder
                /*GameObject buttonPrefab = Resources.Load<GameObject>("Prefabs/UI/Controls/Button");
                if (buttonPrefab == null)
                {
                    Debug.LogError("Failed to load button prefab: Prefabs/UI/Controls/Button");
                    continue;
                }*/

                // Instantiate the button prefab and add it as a child of the VerticalLayoutGroup
                GameObject buttonObj = Instantiate(_bindChainPrefab, layoutObj.transform, true);
                var nameText = buttonObj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
                nameText.text = action.Value.name;

                var bindBtnText = buttonObj.transform.Find("Bind").Find("Text").GetComponent<TextMeshProUGUI>();
                bindBtnText.text = action.Value.bindings[0].effectivePath;

                var bindBtn = buttonObj.transform.Find("Bind").GetComponent<Button>();
                bindBtn.onClick.AddListener(() => InputSys.Bind(action.Value));
                c++;
            
                var layoutRect = layoutObj.GetComponent<RectTransform>();
                //layoutRect.pivot = (Vector2.right/2)+Vector2.up;
                layoutRect.pivot = new Vector2(1,1);
                layoutRect.anchorMin = new Vector2(0.5f,1);
                layoutRect.anchorMax = new Vector2(0.5f,1);
                size = c*(85+ layout.spacing + 10 + 2);
                layoutRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,size);
            
                /*GameObject buttonObj = Instantiate(_bindChainPrefab, layoutObj.transform, true);
                Button button = buttonObj.GetComponent<Button>();

                // Set the button's text to the action name
                Text buttonText = buttonObj.transform.Find("Text").GetComponent<Text>();
                buttonText.text = action.Key;

                // Set the button's callback function to bind the action to a new input
                button.onClick.AddListener(() => InputSys.Bind(action.Value, "Keyboard"));*/
            }
        
            /*var layoutRect = layoutObj.GetComponent<RectTransform>();
            //layoutRect.pivot = (Vector2.right/2)+Vector2.up;
            layoutRect.pivot = new Vector2(1,1);
            layoutRect.anchorMin = new Vector2(0.5f,1);
            layoutRect.anchorMax = new Vector2(0.5f,1);
            layoutRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,c*75+c*layout.spacing+10*2+2);*/   
            //Debug.Log(c*75+c*layout.spacing+10*2+2);
            //contentRect.sizeDelta = new Vector2(contentRect.rect.width,contentRect.rect.height+c*75+c*layout.spacing+12); //
            contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,contentSize);
            Debug.Log(size);
        }
    }

    private GameObject ActParse(InputAction action)
    {
        // Create Bind_Chain object
        GameObject bindChainObj = Instantiate(_bindChainPrefab, _buttonContainer);

        // Set name
        TextMeshProUGUI nameText = bindChainObj.transform.Find("Main/Name").GetComponent<TextMeshProUGUI>();
        nameText.text = action.name;

        // Set bind button
        Button bindButton = bindChainObj.transform.Find("Bind").GetComponent<Button>();
        bindButton.onClick.AddListener(() => InputSys.Bind(action));

        // Set bind text
        TextMeshProUGUI bindText = bindChainObj.transform.Find("Bind/Text").GetComponent<TextMeshProUGUI>();
        bindText.text = action.GetBindingDisplayString();

        return bindChainObj;
    }
}
/*static void PrintDictionary(Dictionary<string, InputAction> actionsList)
{
    string currentKey = null;
    foreach (var entry in actionsList)
    {
        if (currentKey != entry.Key)
        {
            Console.WriteLine($"{entry.Key}");
            currentKey = entry.Key;
        }
        Console.WriteLine($"{entry.Value}");
    }
}*/