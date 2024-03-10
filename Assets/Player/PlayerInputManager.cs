using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls _controls;

    public Vector2 MovementDirection => _controls.Gameplay.Walk.ReadValue<Vector2>();
    public InputAction InteractAction => _controls.Gameplay.Interact;

    public static void SetCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
    }

    public static void SetCursorLockState(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void Start()
    {
        OnEnable();
    }

    private void OnEnable() {
        _controls.Enable();
    }

    private void OnDisable() {
        _controls.Disable();
    }
}
