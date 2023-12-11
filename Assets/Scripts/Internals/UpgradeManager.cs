using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class UpgradeManager : MonoBehaviour
{
    // This is the list of upgradable objects in the scene.
    // Every object in this list must implement the IUpgradable interface.
    // Otherwise, there will be a runtime error.
    [Header("Scene References:")]
    [SerializeReference] private List<Object> _upgradableObjects;

    [SerializeField] private CanvasGroup _upgradeMenu;
    [SerializeField] private Transform _upgradeMenuContent;
    
    [Header("Prefabs:")]
    [SerializeField] private GameObject _upgradableUIPrefab;

    private void Start()
    {
        // Using the LINQ expression .Cast<IUpgradable>(),
        // we can cast the list of objects to a list of IUpgradable.
        var upgradables = _upgradableObjects.Cast<IUpgradable>();
        
        // We loop through each upgradable object in the list,
        // and instantiate a new UpgradableUI prefab for each one in the upgrade container,
        // which is already assumed to be inside the main canvas.
        foreach (var upgradable in upgradables)
        {
            var upgradableUI = Instantiate(_upgradableUIPrefab, _upgradeMenuContent);
            var upgradableUIComponent = upgradableUI.GetComponent<UpgradableUI>();
            
            // We initialize the UpgradableUI component with the IUpgradable reference,
            // and a callback to close the upgrade menu when the upgrade is successful.
            upgradableUIComponent.Initialize(upgradable, 
                onUpgradeSuccessful: () => ToggleUpgradeMenu(false));
        }
    }
    
    /// <summary>
    /// Smoothly fades the upgrade menu in or out.
    /// </summary>
    /// <param name="fadeIn">True to fade in, false to fade out.</param>
    public void ToggleUpgradeMenu(bool fadeIn)
    {
        StartCoroutine(FadeCoroutine());
        return;

        IEnumerator FadeCoroutine()
        {
            var targetAlpha = fadeIn ? 1f : 0f;
            var currentAlpha = _upgradeMenu.alpha;
            var time = 0f;
            
            const float duration = 0.5f;
            
            while (time < duration)
            {
                time += Time.deltaTime;
                _upgradeMenu.alpha = Mathf.Lerp(currentAlpha, targetAlpha, time / duration);
                yield return null;
            }
            _upgradeMenu.alpha = targetAlpha;
            _upgradeMenu.interactable = fadeIn;
            _upgradeMenu.blocksRaycasts = fadeIn;
        }
    }
}
