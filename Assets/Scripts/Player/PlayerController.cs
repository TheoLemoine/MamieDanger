using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private NavMeshAgent _agent;
    private Camera _cam;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float maxRaycastDistance = 100f;

    private Vector2 _cursorPos;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _cam = Camera.main;
    }

    public void OnClick()
    {
        RaycastHit hit;
        var ray = _cam.ScreenPointToRay(_cursorPos);

        if (Physics.Raycast(ray, out hit, maxRaycastDistance, groundLayer))
        {
            _agent.destination = hit.point;
        }
    }

    public void OnCursorMove(InputAction.CallbackContext value)
    {
        _cursorPos = value.ReadValue<Vector2>();
    }
}
