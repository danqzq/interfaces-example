using UnityEngine;

public class PlayerHealth : MonoBehaviour, IUpgradable
{
    [field: SerializeField] public int Health { get; set; } = 100;
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    
    [SerializeField] private Sprite _healthIcon;

    public UpgradableInfo GetInfo() => new UpgradableInfo
    {
        name = "Health",
        description = "Increases the player's health",
        icon = _healthIcon
    };

    public void OnUpgrade()
    {
        Health += 50;
        MaxHealth += 50;
    }
}