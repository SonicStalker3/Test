using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogyScript : MonoBehaviour
{
    [SerializeField, Tooltip("Сюда нужно положить корневой файл диалога('DialogObject'(Instance of Scriptable Object))")]
    private DialogObject dialog;

    private InputSys _input;

    private Button DiagPanel;
    private GameObject HistoryPanel;
    private GameObject HistoryPanelView;
    private GameObject[] DiagList;
    private GameObject HistoryChoice;

    public Text NameField;
    public Text DialogField;
    
    private bool is_Triggert;
    //private bool prev_state = false;

    public delegate void EndDialogEvent();
    public EndDialogEvent OnDialogEnd;
    private Player _player;

    private void Awake()
    {
        HistoryChoice = Resources.Load<GameObject>("HistoryDialogText");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_input != null)
        {
            Debug.Log(_input.historyBtn);
            if (_input.historyBtn) 
            {
                HistoryToogle();
            }
            if (_input.nextBtn)
            {
                var dialogTxt = dialog.Next();
                if (dialogTxt != null)
                {
                    NameField.text = dialogTxt.who;
                    DialogField.text = dialogTxt.text;

                }
                else
                {
                    OnDialogEnd.Invoke();
                    //OnDialogEnd -= Player.OnEndDialog;
                }
            }
           
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        _player = other.transform.GetComponent<Player>();
        if (_player != null && !is_Triggert)
        {
            _player.isControll = false;
            OnDialogEnd += _player.OnEndDialog;
            _input = _player.GetComponent<InputSys>();
            DiagPanel = other.transform.GetComponent<Player>().DiagPanel.GetComponent<Button>();
            HistoryPanel = other.transform.GetComponent<Player>().HistoryPanel;
            HistoryPanelView = other.transform.GetComponent<Player>().HistoryPanelView;
            is_Triggert = true;
            if (DiagPanel != null) DiagPanel.onClick.AddListener(NextMsg);
            //_input._input.actions["NextBtn"].performed += NextMsg;
            dialog.Reset();
            if (DiagList is null)
            {
                DiagList = new GameObject[dialog.dialog.Length];
                for (int i = 0; i < dialog.dialog.Length; i++)
                {
                    var x = Instantiate(HistoryChoice, HistoryPanelView.transform);
                    x.SetActive(false);
                    var i1 = i;
                    x.GetComponent<Button>().onClick.AddListener(() =>{MsgChange(i1); });
                    x.GetComponentInChildren<TextMeshProUGUI>().text = dialog.dialog[i].who + ": " + dialog.dialog[i].text;
                    DiagList[i] = x;
                }
            }

            NextMsg();
        }
    }

    private void HistoryUpdate()
    {
        for (int i = 0; i < dialog.iteration; i++)
        {
            DiagList[i].SetActive(true);
        }
    }
    private void NextMsg()
    {
        var dialogTxt = dialog.Next();
        if (dialogTxt !=null)
        {
            NameField.text = dialogTxt.who;
            DialogField.text = dialogTxt.text;
            HistoryUpdate();
        }
        else
        {
            OnDialogEnd.Invoke();
        }

        
    }
    private void HistoryToogle() 
    {
        // if (DiagList is null)
        // {
        //     DiagList = new GameObject[dialog.dialog.Length];
        //     for (int i = 0; i < dialog.dialog.Length; i++)
        //     {
        //         var x = Instantiate(HistoryChoice, HistoryPanelView.transform);
        //         x.SetActive(false);
        //         var i1 = i;
        //         x.GetComponent<Button>().onClick.AddListener(() =>{MsgChange(i1); });
        //         x.GetComponentInChildren<TextMeshProUGUI>().text = dialog.dialog[i].who + ": " + dialog.dialog[i].text;
        //         DiagList[i] = x;
        //     }
        // }

        HistoryUpdate();
        HistoryPanel.SetActive(!HistoryPanel.activeSelf);
    }

    public void MsgChange(int s)
    {
        var x = dialog.Set(s);
        NameField.text = x.who;
        DialogField.text = x.text;
        Debug.Log(s);
        HistoryUpdate();
    }

    /*    private void NextMsg(InputAction.CallbackContext context)
        {
            var dialogTxt = dialog.Next();
            if (dialogTxt !=null)
            {
                NameField.text = dialogTxt.who;
                DialogField.text = dialogTxt.text;

            }
            else
            {
                OnDialogEnd.Invoke();
                //OnDialogEnd -= Player.OnEndDialog;
            }
            Debug.Log("1");

        }*/
}
