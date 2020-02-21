using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private NavMeshAgent _agent;
    private Camera _cam;
    private LayerMask _groundLayer;

    private Vector2 _cursorPos;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _cam = Camera.main;
        
        _groundLayer = LayerMask.GetMask("Ground");
    }

    public void OnClick()
    {
        RaycastHit hit;
        var ray = _cam.ScreenPointToRay(_cursorPos);

        if (Physics.Raycast(ray, out hit, _groundLayer))
        {
            _agent.destination = hit.point;
        }
    }

    public void OnCursorMove(InputAction.CallbackContext value)
    {
        _cursorPos = value.ReadValue<Vector2>();
    }
}
