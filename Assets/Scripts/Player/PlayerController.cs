using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Global;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float maxRaycastDistance = 100f;

        private NavMeshAgent _agent;
        private Camera _cam;

        private Vector2 _pointer;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            if (InputManager.IsReady)
            {
                InputManager.ActionMaps.Player.Move.started += Move;
                InputManager.ActionMaps.Player.UpdatePointer.performed += UpdatePointer;
            }
        }

        private void OnDisable()
        {
            if (InputManager.IsReady)
            {
                InputManager.ActionMaps.Player.Move.started -= Move;
                InputManager.ActionMaps.Player.UpdatePointer.performed -= UpdatePointer;
            }
        }

        private void UpdatePointer(InputAction.CallbackContext value)
        {
            _pointer = value.ReadValue<Vector2>();
        }
        
        private void Move(InputAction.CallbackContext value)
        {
            var ray = _cam.ScreenPointToRay(_pointer);

            if (Physics.Raycast(ray, out var hit, maxRaycastDistance, groundLayer))
            {
                _agent.SetDestination(hit.point);
            }
        }

    }
}
