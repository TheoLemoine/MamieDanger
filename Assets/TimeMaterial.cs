using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMaterial : MonoBehaviour
{
    [SerializeField] private string durationPropCode = "_duration";
    [SerializeField] private string startTimePropCode = "_startTime";
    private Material _material;
    private float _duration;
    private int _startTimePropId;
    
    void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        _material = meshRenderer != null ? meshRenderer.material : GetComponent<Image>().material;
        
        _startTimePropId = Shader.PropertyToID(startTimePropCode);
        _duration = _material.GetFloat(Shader.PropertyToID(durationPropCode));
    }

    public void TriggerTime()
    {
        _material.SetFloat(_startTimePropId, Time.time);
    }
}
