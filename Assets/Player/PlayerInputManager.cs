using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerControls _controls;

    public Vector2 MovementDirection => _controls.Movement.Walk.ReadValue<Vector2>();

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
