using TMPro;
using UnityEngine;

namespace Entities
{
    /// <summary>
    ///Entity with Status Bar(With Stats Panel) Behaviour
    /// </summary>
    public abstract class StatsEntity : Entity
    {
        protected int stamina;
        [SerializeField]
        protected string ST_Name = "";

        [SerializeField] private Canvas UI;
        [SerializeField]
        private TextMeshProUGUI nameLabel;

        protected string NameFormat;
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
            NameFormat = "{0}: {1}/{2}";
            animator = GetComponentInChildren<Animator>();
            mainCamera = Camera.main;
            if (string.IsNullOrEmpty(ST_Name))
            {
                ST_Name = E_name;
            }
            UI = GetComponentInChildren<Canvas>();
            nameLabel = UI.GetComponentInChildren<TextMeshProUGUI>();
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
            nameLabel.text = string.Format(NameFormat,new object[]{ST_Name,Health,MaxHealth});
            //nameLabel.text = "{ST_Name}: {Health}/{MaxHealth}";
        }
    }
}
