using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))] // This script must be attached to a UI element with a Graphic component.
public class HoverPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // This property is used to set the text of the panel.
    public string Text
    {
        get => _textMeshPro.text;
        set => _textMeshPro.text = value;
    }
    
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    
    // This method is called when the mouse enters the UI element.
    public void OnPointerEnter(PointerEventData eventData)
    {
        _panel.SetActive(true);
    }

    // This method is called when the mouse exits the UI element.
    public void OnPointerExit(PointerEventData eventData)
    {
        _panel.SetActive(false);
    }
}