using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputManager))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInputManager _inputManager;

    [Header("Script Settings")]
    [SerializeField]
    private Transform cameraTransform;
    
    [Header("Movement Settings")]
    [SerializeField]
    private float movementSpeed = 5f;

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
        void MakeMonsterMoveToTargetLocation()
        {
            Physics.Raycast(new Ray(cameraTransform.position, cameraTransform.forward), out var hit);

            var xGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            xGameObject.transform.position = hit.point;
            xGameObject.transform.localScale = Vector3.one * 0.25f;

            var monster = FindFirstObjectByType<MonsterAI>();
            monster.GoToPoint(hit.point);
        }
        
        MakeMonsterMoveToTargetLocation();
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _inputManager = GetComponent<PlayerInputManager>();
        
        _inputManager.InteractAction.performed += Interacted;
        
        PlayerInputManager.SetCursorVisibility(false);
        PlayerInputManager.SetCursorLockState(CursorLockMode.Locked);
    }
}
