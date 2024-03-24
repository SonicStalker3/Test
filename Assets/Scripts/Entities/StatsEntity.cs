using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public abstract class StatsEntity : Entity
{
    protected int stamina;
    [SerializeField]
    private TextMeshProUGUI nameLabel;
    public int Stamina => stamina;
    [Header("Stats Properties")]
    public int baseDamage = 10;
    [SerializeField] private Camera mainCamera;
    protected Color name_color;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    protected new void Init()
    {
        base.Init();
        mainCamera = Camera.main;
        nameLabel = GetComponentInChildren<TextMeshProUGUI>();
        nameLabel.color = name_color;
        Debug.Log($"{name_color} ?= {nameLabel.color}");
    }

    protected void UpdateGUI() 
    {
        var rotation = mainCamera.transform.rotation;
        nameLabel.transform.LookAt(nameLabel.transform.position + rotation * Vector3.forward,
            rotation * Vector3.up);
        nameLabel.text = $"{E_name}: {Health}/{MaxHealth}";
    }
}
