using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class UISlide : MonoBehaviour
    {
        [SerializeField] [Range(0.2f, 2f)] float slideDuration = 1f;
        [SerializeField] private Ease easeType = Ease.OutQuad;
        [SerializeField] private Vector2 offsetWhenVisible;
        [SerializeField] private Vector2 offsetWhenNotVisible;
        [SerializeField] private bool startVisible;
        [SerializeField] private bool animateAtStart;
        [SerializeField] private bool stopAnimationWhenPaused;

        private bool _isVisible;
        private RectTransform _rectTransform;
        private Vector3 _startPosition;
    
        private void Start()
        {
            _isVisible = animateAtStart ? !startVisible : startVisible;
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.localPosition;
        
            if (animateAtStart)
            {
                if (_isVisible)
                    SlideOut();
                else
                    SlideIn();
            }
            else
            {
                _rectTransform.localPosition +=
                    _isVisible
                        ? new Vector3(offsetWhenVisible.x, offsetWhenVisible.y, 0f)
                        : new Vector3(offsetWhenNotVisible.x, offsetWhenNotVisible.y, 0f);
            }
        }
    
        public void SlideIn()
        {
            _isVisible = true;
            _rectTransform.localPosition = _startPosition + new Vector3(offsetWhenNotVisible.x, offsetWhenNotVisible.y, 0f);
            var localEndPos = _startPosition + new Vector3(offsetWhenVisible.x, offsetWhenVisible.y, 0f);
            _rectTransform.DOLocalMove(localEndPos, slideDuration)
                .SetUpdate(!stopAnimationWhenPaused)
                .SetEase(easeType);
        }

        public void SlideOut()
        {
            _isVisible = false;
            _rectTransform.localPosition = _startPosition + new Vector3(offsetWhenVisible.x, offsetWhenVisible.y, 0f);
            var localEndPos = _startPosition + new Vector3(offsetWhenNotVisible.x, offsetWhenNotVisible.y, 0f);
            _rectTransform.DOLocalMove(localEndPos, slideDuration)
                .SetUpdate(!stopAnimationWhenPaused)
                .SetEase(easeType);
        }
    }
}
