using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Image _armorBarFill;
    
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _armorText;
    [SerializeField] private TextMeshProUGUI _swordDamageText;
    
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerArmor _playerArmor;
    [SerializeField] private Sword _sword;
    
    private float _health;
    private float _armor;
    
    private void Start()
    {
        _health = _playerHealth.Health;
        _armor = _playerArmor.Armor;
    }

    private void Update()
    {
        const float lerpSpeed = 10f;
        
        _health = Mathf.Lerp(_health, _playerHealth.Health, Time.deltaTime * lerpSpeed);
        _healthBarFill.fillAmount = _health / _playerHealth.MaxHealth;
        _healthText.text = $"{Mathf.RoundToInt(_health)} / {_playerHealth.MaxHealth}";
        
        _armor = Mathf.Lerp(_armor, _playerArmor.Armor, Time.deltaTime * lerpSpeed);
        _armorBarFill.fillAmount = _armor / _playerArmor.MaxArmor;
        _armorText.text = $"{Mathf.RoundToInt(_armor)} / {_playerArmor.MaxArmor}";
        
        _swordDamageText.text = $"Sword Damage: {_sword.Damage}";
    }
}