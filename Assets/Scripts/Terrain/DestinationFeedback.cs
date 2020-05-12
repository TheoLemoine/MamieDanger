using UnityEngine;

public class DestinationFeedback : MonoBehaviour
{
    [SerializeField] private string durationPropCode;
    [SerializeField] private string startTimePropCode;
    [SerializeField][Range(1f, 2f)] private float newRaycastLength = 1.5f;
    [SerializeField][Range(0f, 1f)] private float maxDistanceFromExpectation = 0.1f;
    [SerializeField][Range(0f, 1f)] private float slopeThreshold = 0.2f;
    [SerializeField][Range(0.01f, 0.1f)] private float slopeOffset = 0.02f;
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

    public void OnSetDestination(Vector3 newDestination, RaycastHit hit)
    {
        if (Vector3.Distance(newDestination, hit.point) > maxDistanceFromExpectation 
            || (1 - Vector3.Dot(Vector3.up, hit.normal)) > slopeThreshold)
        {
            var ray = new Ray(newDestination + Vector3.up / 2f + hit.normal * slopeOffset, Vector3.down);
            Physics.Raycast(ray, out hit, newRaycastLength, 1 << hit.collider.gameObject.layer);
        }
        var normal = hit.normal;
        var pos = hit.point;
        
        _meshRenderer.enabled = true;
        _transform.position = pos + normal * 0.01f;
        _transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);
        _meshRenderer.material.SetFloat(_startTimePropId, Time.time);
    }
}
