using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradableUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _upgradeButton;
    
    public void Initialize(IUpgradable upgradable, System.Action onUpgradeSuccessful)
    {
        var info = upgradable.GetInfo();
        _nameText.text = info.name;
        _iconImage.sprite = info.icon;
        
        _iconImage.GetComponent<HoverPanel>().Text = info.description;
        
        _upgradeButton.onClick.AddListener(() =>
        {
            upgradable.OnUpgrade();
            _upgradeButton.interactable = false;
            onUpgradeSuccessful?.Invoke();
        });
    }
}