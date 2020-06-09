using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class TargetDirection : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasTransform;
        [SerializeField] [Range(0f, 200f)] private float margin = 30f;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private UnityEvent enterScreenEvent;
        [SerializeField] private UnityEvent exitScreenEvent;
        private Camera _camera;
        private RectTransform _indicatorTransform;
        private bool _isInScreen;

        private void Start()
        {
            _camera = Camera.main;
            _indicatorTransform = GetComponent<RectTransform>();
            if (enterScreenEvent == null) enterScreenEvent = new UnityEvent();
            if (exitScreenEvent == null) exitScreenEvent = new UnityEvent();
        }

        private void Update()
        {
            var screenSize = new Vector2(Screen.width, Screen.height);
            var screenCenter = screenSize / 2f;

            var targetScreenPoint = (Vector2)_camera.WorldToScreenPoint(targetTransform.position);
            var cursorPoint = targetScreenPoint - screenCenter;

            var cursorBox = screenSize - Vector2.one * (margin * 2f);
            var cursorBoxCenter = cursorBox / 2f;

            var wasInScreen = _isInScreen;
            _isInScreen = targetScreenPoint.x > margin && targetScreenPoint.x < cursorBox.x + margin && targetScreenPoint.y < cursorBox.y + margin && targetScreenPoint.y > margin;

            if (wasInScreen != _isInScreen)
                if (_isInScreen) enterScreenEvent.Invoke();
                else exitScreenEvent.Invoke();
            
            if (_isInScreen) {
                _indicatorTransform.localPosition = cursorPoint / canvasTransform.localScale;
                return;
            }

            var cameraTransform = _camera.transform;
            var angle = Mathf.Atan2(cursorPoint.y, cursorPoint.x);
            // Revert angle if the target is behind the camera  
            if(Vector3.Dot(cameraTransform.forward, targetTransform.position - cameraTransform.position) < 0f) angle -= Mathf.PI;
        
            var cornerAngle = Mathf.Atan2(cursorBoxCenter.x, cursorBoxCenter.y);
        
            var newPos = Vector2.zero;
            // Right
            if (Mathf.Abs(angle) < Mathf.PI / 2f - cornerAngle)
                newPos = new Vector2(cursorBoxCenter.x, Mathf.Tan(angle) * cursorBoxCenter.x);
            // Top
            else if (angle > Mathf.PI / 2f - cornerAngle && angle < Mathf.PI / 2f + cornerAngle)
                newPos = new Vector2(Mathf.Tan(angle - Mathf.PI / 2f) * -cursorBoxCenter.y, cursorBoxCenter.y);
            // Left
            else if (Mathf.Abs(angle) > Mathf.PI / 2f + cornerAngle)
                newPos = new Vector2(-cursorBoxCenter.x, Mathf.Tan(angle) * -cursorBoxCenter.x);
            // Bottom
            else if (angle > -Mathf.PI / 2f - cornerAngle && angle < -Mathf.PI / 2f + cornerAngle)
                newPos = new Vector2(Mathf.Tan(angle - Mathf.PI / 2f) * cursorBoxCenter.y, -cursorBoxCenter.y);
        
            _indicatorTransform.localPosition = newPos / canvasTransform.localScale;
            _indicatorTransform.localRotation = Quaternion.Euler(0,0, angle * Mathf.Rad2Deg);
        }

    }
}
