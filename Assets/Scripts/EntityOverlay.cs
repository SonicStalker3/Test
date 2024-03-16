using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntityOverlay : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public Image HPImage;
    public string HealthPref;
    
    public TextMeshProUGUI Stamina;
    public Image StaminaImage;
    public string StaminaPref;
    
    
    private Player _player;
    // Start is called before the first frame update
    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HP) HP.text = HealthPref+_player.Health;
        if (HPImage) HPImage.fillAmount = (float)_player.Health / _player.MaxHealth;
        if (Stamina) Stamina.text = StaminaPref + _player.Stamina;
        if (StaminaImage) StaminaImage.fillAmount = (float)_player.Stamina / _player.MaxStamina;
    }
}
