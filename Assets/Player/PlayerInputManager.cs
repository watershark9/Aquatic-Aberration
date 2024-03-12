using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls _controls;

    public Vector2 MovementDirection => _controls.Gameplay.Walk.ReadValue<Vector2>();
    public InputAction InteractAction => _controls.Gameplay.Interact;
    public InputAction PauseAction => _controls.Gameplay.Pause;

    public static void SetCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
    }

    public static void SetCursorLockState(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }

    public static void LockAndHideCursor()
    {
        SetCursorLockState(CursorLockMode.Locked);
        SetCursorVisibility(false);
    }

    public static void FreeAndShowCursor()
    {
        SetCursorLockState(CursorLockMode.None);
        SetCursorVisibility(true);
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
