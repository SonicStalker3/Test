using System;
using System.Collections;
using System.Collections.Generic;
using Entities;
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

    public void OpenDialog(Player player, DialogObject dialog, Animator NPCanimator)
    {
        if (player != null && !_isTriggert)
        {
            player.isControll = false;
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

            this._npcAnimator = NPCanimator;
            NextMsg();
        }
    }

    private void NextMsg()
    {
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
        _npcAnimator.SetBool("IsEnded", false);
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