using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls _controls;

    public Vector2 MovementDirection => _controls.Movement.Walk.ReadValue<Vector2>();

    public void SetCursorVisibility(bool isVisible)
    {
        Cursor.visible = isVisible;
    }

    public void SetCursorLockState(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void Start()
    {
        _controls.Enable();
    }

    private void OnEnable() {
        _controls.Enable();
    }

    private void OnDisable() {
        _controls.Disable();
    }
}
