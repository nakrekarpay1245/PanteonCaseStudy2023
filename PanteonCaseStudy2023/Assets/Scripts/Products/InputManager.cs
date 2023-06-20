using System;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    /// <summary>
    /// Event triggered when a left mouse click occurs.
    /// </summary>
    public event Action OnLeftMouseClick;

    /// <summary>
    /// Event triggered when a right mouse click occurs.
    /// </summary>
    public event Action OnRightMouseClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnLeftMouseClick?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            OnRightMouseClick?.Invoke();
        }
    }
}
