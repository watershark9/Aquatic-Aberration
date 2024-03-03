using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputManager _inputManager;

    [Header("Script Settings")]
    [SerializeField]
    private Transform cameraTransform;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5f;

    private void Move()
    {
        var inputDirection = _inputManager.MovementDirection;
        if (!(inputDirection.magnitude > 0.1f)) return;

        var movementDirection = (cameraTransform.forward * inputDirection.y + cameraTransform.right * inputDirection.x).normalized;
        movementDirection.y = 0;

        var position = transform.position;
        var targetPosition = position + movementDirection;
        var maxTravelDistanceThisFrame = movementSpeed * Time.deltaTime;
        position = Vector3.MoveTowards(position, targetPosition, maxTravelDistanceThisFrame);
        transform.position = position;
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Start()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        
        _inputManager.SetCursorVisibility(false);
        _inputManager.SetCursorLockState(CursorLockMode.Locked);
    }
}
