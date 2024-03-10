using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CollectableCounterUIUpdater : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    
    private TextMeshProUGUI _textBox;

    private void UpdateCounter()
    {
        _textBox.text = playerInventory.ItemCount + "/5";
    }

    public void Awake()
    {
        _textBox = GetComponent<TextMeshProUGUI>();
        playerInventory.ItemAdded?.AddListener(UpdateCounter);
    }
}
