using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int ItemCount { get; private set; } = 0;
    public readonly UnityEvent ItemAdded = new();

    public void AddItem()
    {
        ItemCount++;
        ItemAdded?.Invoke();
    }
}
