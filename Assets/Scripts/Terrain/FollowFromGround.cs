using UnityEngine;
using Utils;

namespace Terrain
{
    public class FollowFromGround : MonoBehaviour
    {

        [SerializeField] private Transform objectToFollow;
        [SerializeField] private SingleUnityLayer groundLayer;
        [SerializeField] private float maxRaycastDistance = 100f;
        [SerializeField] private Vector3 raycastOriginAdjustment;
        [SerializeField] [Range(0.001f, 0.05f)] private float normalOffset = 0.005f;
        [SerializeField] private bool keepObjectRotation = true;
        [SerializeField] private Vector3 objectRotationAdjustment;
        private Transform _transform;
        private bool _objHasBeenDestroy;
    
        void Start()
        {
            _transform = transform;
        }

        void Update()
        {
            _objHasBeenDestroy = _objHasBeenDestroy || objectToFollow == null;
            if (_objHasBeenDestroy) return;
            
            // Cast a ray to the ground where the quad should go
            if (Physics.Raycast(objectToFollow.position + objectToFollow.TransformVector(raycastOriginAdjustment), Vector3.down, out var hit, maxRaycastDistance, groundLayer.Mask))
            {
                _transform.position = hit.point + hit.normal * normalOffset;
                // Quad rotation can follow object y rotation with "keepObjectRotation" var
                var yRotation = keepObjectRotation ? objectToFollow.rotation.eulerAngles.y : 0f;
                // Quad rotation take into account the slope of the floor
                _transform.rotation = Quaternion.LookRotation(hit.normal, Vector3.up) * Quaternion.Euler(objectRotationAdjustment) * Quaternion.AngleAxis(yRotation, Vector3.up);
            }
        }
    }
}
