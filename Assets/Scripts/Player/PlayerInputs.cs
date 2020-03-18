using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputs : MonoBehaviour
    {
        [Serializable] public class Vector2UnityEvent: UnityEvent<Vector2> {}
        
        [SerializeField] public Vector2UnityEvent onTap;
        [HideInInspector] public Vector2 currentPos = new Vector2(0, 0);

        public void OnClick()
        {
            onTap.Invoke(currentPos);
        }

        public void OnCursorMove(InputAction.CallbackContext value)
        {
            currentPos = value.ReadValue<Vector2>();
        }
    }
}