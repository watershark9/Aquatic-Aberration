using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour
{
    public readonly UnityEvent Interacted = new();

    [SerializeField] private MonsterAI monster;
    [SerializeField] private AudioSource pickupSfx;
    
    public void Awake()
    {
        var rendererComponent = GetComponent<Renderer>();
        var colliderComponent = GetComponent<Collider>();
        
        Interacted?.AddListener(() =>
        {
            rendererComponent.enabled = false;
            colliderComponent.enabled = false;
        });
        Interacted?.AddListener(() =>
        {
            monster.Alerted.Invoke(transform.position);
        });
        Interacted?.AddListener(() =>
        {
            pickupSfx.time = 0;
            pickupSfx.Play();
        });
    }
}
