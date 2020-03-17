using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPositionToShader : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private string propKey;
    private Vector4 _posVector = new Vector4();
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        UpdatePosVector();
    }

    private void UpdatePosVector()
    {
        Vector3 p = _transform.position;
        _posVector.Set(p.x, p.y, p.z, 0);
    }

    private void Update()
    {
        UpdatePosVector();
        material.SetVector(propKey, _posVector);
    }
}
