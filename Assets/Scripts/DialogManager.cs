using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Interfaces.Entities;
using Scriptable.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Dialog Panel Manager
/// </summary>
public class DialogManager : MonoBehaviour
{
    [SerializeField,
     Tooltip("Сюда нужно положить корневой файл диалога('DialogObject'(Instance of Scriptable Object))")]
    private DialogObject dialog;

    private Button _diagPanel;

    private GameObject _historyPanel;
    private GameObject _historyPanelView;
    private GameObject[] _diagList;
    private GameObject _historyChoice;

    public Text NameField;
    public Text DialogField;

    public delegate void EndDialogEvent();
    public EndDialogEvent OnDialogEnd;
    
    public delegate void NextDialogEvent();
    public EndDialogEvent OnNextDialog;

    private bool _isTriggert = false;
    private ISpeakable[] _speakers;
    
    private Animator _npcAnimator;

    private void Awake()
    {
        _historyChoice = Resources.Load<GameObject>("HistoryDialogText");
        NpcManager.RegisterDialogManager(this);
        //NameField =
        //DialogField;
    }

    public void Update()
    {
        if (InputSys.HistoryBtn)
        {
            HistoryToogle();
        }

        if (InputSys.NextBtn)
        {
            NextMsg();
            var dialogTxt = dialog.Next();
            if (dialogTxt != null)
            {
                NameField.text = dialog.names[dialogTxt.who_id];
                DialogField.text = dialogTxt.text;
            }
            else
            {
                OnDialogEnd.Invoke();
                //OnDialogEnd -= Player.OnEndDialog;
            }
        }
    }

    public void AddSpeaker(int id, ISpeakable speaker)
    {
        if (id>=0 && id <_speakers.Length-1) _speakers[id] = speaker;
        //if (_speakers[_speakers.Length-1] != null) {NextMsg(); Debug.Log("Все готовы");}
        //Debug.Log(_speakers.Length);
    }   

    public void OpenDialog(Player player, DialogObject dialog, Animator npcAnimator, int count)
    {
        if (player != null && !_isTriggert)
        {
            _speakers = new ISpeakable[count];
            
            player.DialogySpeach();
            
            _npcAnimator = npcAnimator;
            _npcAnimator.SetBool("IsNewDialog",false);
            _npcAnimator.SetBool("IsEnded", false);
            player.isDialog = true;
           
            if (_diagPanel is null)
            {
                OnDialogEnd += player.OnEndDialog;
                _diagPanel = player.DiagPanel.GetComponent<Button>();
                _historyPanel = player.HistoryPanel;
                _historyPanelView = player.HistoryPanelView;
            }
            _isTriggert = true;

            this.dialog = dialog;
            if (this.dialog == null) throw new Exception($"Dialog not found");
            this.dialog.Reset();

            if (_diagPanel != null) _diagPanel.onClick.AddListener(NextMsg);
            
            if (_diagList is null)
            {
                _diagList = new GameObject[this.dialog.dialog.Length];
                for (int i = 0; i < this.dialog.dialog.Length; i++)
                {
                    var x = Instantiate(_historyChoice, _historyPanelView.transform);
                    x.SetActive(false);
                    var i1 = i;
                    x.GetComponent<Button>().onClick.AddListener(() => { MsgChange(i1); });
                    x.GetComponentInChildren<TextMeshProUGUI>().text = NameField + ": " + DialogField;
                    _diagList[i] = x;
                }
            }
            MsgChange(0);
        }
    }

    private void NextMsg()
    {
        if (_speakers[0] == null) throw new Exception($"Speakers not found");
        var dialogTxt = dialog.Next();
        if (dialogTxt != null)
        {
            StartCoroutine(nameof(Dialog), dialogTxt);
            /*animator.SetBool("IsSpeech",true);
            NameField.text = dialogTxt.who;
            DialogField.text = dialogTxt.text;
            HistoryUpdate();
            animator.SetBool("IsSpeech",false);*/
        }
        else CloseDialog();
    }


    private void HistoryToogle()
    {
        HistoryUpdate();
        _historyPanel.SetActive(!_historyPanel.activeSelf);
    }

    private IEnumerator Dialog(DialogText dialogTxt)
    {
        _npcAnimator.SetFloat("SayBlend", Random.value);
        _npcAnimator.SetBool("IsSpeech", true);
        NameField.text = dialog.names[dialogTxt.who_id];
        DialogField.text = dialogTxt.text;
        _speakers[0].DialogySpeach();
        HistoryUpdate();
        yield return new WaitForSeconds(5);
        _npcAnimator.SetBool("IsSpeech", false);
        yield break;
    }

    private IEnumerator DialogEnd()
    {
        yield return new WaitForSeconds(2);
        _npcAnimator.SetBool("IsEnded", true);
        OnDialogEnd.Invoke();
        yield break;
    }

    private void MsgChange(int s)
    {
        var x = dialog.Set(s);
        NameField.text = dialog.names[x.who_id];
        DialogField.text = x.text;
        Debug.Log(s);
        HistoryUpdate();
    }

    private void HistoryUpdate()
    {
        for (int i = 0; i < dialog.iteration; i++)
        {
            _diagList[i].SetActive(true);
        }
    }

    private void CloseDialog()
    {
        StartCoroutine(nameof(DialogEnd));
    }
}