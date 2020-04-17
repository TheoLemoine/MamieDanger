using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationFeedback : MonoBehaviour
{
    [SerializeField] private string durationPropCode;
    [SerializeField] private string startTimePropCode;
    private MeshRenderer _meshRenderer;
    private Transform _transform;
    private float _duration;
    private int _startTimePropId;
    
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
        _transform = transform;
        _startTimePropId = Shader.PropertyToID(startTimePropCode);
        _duration = _meshRenderer.material.GetFloat(Shader.PropertyToID(durationPropCode));
    }

    public void OnSetDestination(Vector3 pos, Vector3 normal)
    {
        _meshRenderer.enabled = true;
        _transform.position = pos + normal * 0.01f;
        _transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);
        _meshRenderer.material.SetFloat(_startTimePropId, Time.time);
    }
}
