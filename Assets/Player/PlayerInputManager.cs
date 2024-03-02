using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls _controls;

    public Vector2 MovementDirection => _controls.Movement.Walk.ReadValue<Vector2>();

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
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
