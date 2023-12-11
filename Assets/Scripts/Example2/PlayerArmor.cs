using UnityEngine;

public class PlayerArmor : MonoBehaviour, IUpgradable
{
    [field: SerializeField] public int Armor { get; set; } = 50;
    [field: SerializeField] public int MaxArmor { get; private set; } = 50;
    
    [SerializeField] private Sprite _armorIcon;

    public UpgradableInfo GetInfo() => new UpgradableInfo
    {
        name = "Armor",
        description = "Increases the player's armor",
        icon = _armorIcon
    };

    public void OnUpgrade()
    {
        Armor += 25;
        MaxArmor += 25;
    }
}