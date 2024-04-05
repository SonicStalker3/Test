using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

///Entity with Status Bar(With Stats Panel) Behaviour
public abstract class StatsEntity : Entity
{
    protected int stamina;
    [SerializeField]
    protected string ST_Name = "";

    [SerializeField] private Canvas UI;
    [SerializeField]
    private TextMeshProUGUI nameLabel;
    public int Stamina => stamina;
    [Header("Stats Properties")]
    public int baseDamage = 10;
    [SerializeField] private Camera mainCamera;
    protected Color name_color;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    protected new void Init()
    {
        base.Init();
        animator = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
        if (ST_Name.Length < 1)
        {
            ST_Name = E_name;
        }
        nameLabel = GetComponentInChildren<TextMeshProUGUI>();
        UI = GetComponentInChildren<Canvas>();
        nameLabel.color = name_color;
        Debug.Log($"{name_color} ?= {nameLabel.color}");
    }

    protected void UpdateGUI() 
    {
        var rotation = mainCamera.transform.rotation;
        UI.transform.LookAt(nameLabel.transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        //Vector3 directionToNameLabel = nameLabel.transform.position - UI.transform.position;
        //Quaternion rotation1 = Quaternion.LookRotation(directionToNameLabel, Vector3.up);
        //UI.transform.rotation = rotation1;
        //nameLabel.transform.LookAt(nameLabel.transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        nameLabel.text = $"{ST_Name}: {Health}/{MaxHealth}";
    }
}
