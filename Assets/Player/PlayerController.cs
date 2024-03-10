using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInputManager _inputManager;
    private PlayerInventory _inventory;

    [Header("Script Settings")]
    [SerializeField]
    private Transform cameraTransform;
    
    [Header("Movement Settings")]
    [SerializeField]
    private float movementSpeed = 5f;

    [Header("Interaction Settings")]
    [SerializeField]
    private float maxInteractionDistance = 3f;

    private void Move()
    {
        var inputDirection = _inputManager.MovementDirection;
        
        var netMovement = Physics.gravity;
        
        if (inputDirection.magnitude > 0.1f)
        {
            var movementDirection =
                (cameraTransform.forward * inputDirection.y + cameraTransform.right * inputDirection.x).normalized;

            netMovement += movementDirection * movementSpeed;
        }

        _characterController.Move(netMovement * Time.deltaTime);
    }
    
    private void Interacted(InputAction.CallbackContext obj)
    {
        var ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Physics.Raycast(ray, out var hit);
        if (hit.collider == null) return;
        
        var interactionObject = hit.collider.gameObject;
        if (Vector3.Distance(interactionObject.transform.position, cameraTransform.position) >
            maxInteractionDistance) return;
        
        if (!interactionObject.gameObject.TryGetComponent<CollectableItem>(out var itemComponent)) return;

        itemComponent.Interacted?.Invoke();
        _inventory.AddItem();

    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputManager = GetComponent<PlayerInputManager>();
        _inventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        _inputManager.InteractAction.performed += Interacted;
        
        PlayerInputManager.SetCursorVisibility(false);
        PlayerInputManager.SetCursorLockState(CursorLockMode.Locked);
    }
}
