using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class UIScale : MonoBehaviour
    {
        [SerializeField] [Range(0.2f, 2f)] float scaleDuration = 1f;
        [SerializeField] private Ease easeType = Ease.OutQuad;
        [SerializeField] private float shrunkSize;
        [SerializeField] private bool startShrunk;
        [SerializeField] private bool animateAtStart;
        [SerializeField] private bool stopAnimationWhenPaused;

        private bool _isFullScaled;
        private RectTransform _rectTransform;
        private Vector3 _fullSize;
        private Vector3 _shrunkSize;
    
        private void Start()
        {
            _isFullScaled = animateAtStart ? startShrunk : !startShrunk;
            _rectTransform = GetComponent<RectTransform>();
            _fullSize = _rectTransform.localScale;
            _shrunkSize = new Vector3(shrunkSize, shrunkSize, shrunkSize);
        
            if (animateAtStart)
            {
                if (_isFullScaled)
                    ScaleOut();
                else
                    ScaleIn();
            }
            else
            {
                _rectTransform.localScale = _isFullScaled ? _fullSize : _shrunkSize;
            }
        }
    
        public void ScaleIn()
        {
            _isFullScaled = true;
            _rectTransform.localScale = _shrunkSize;
            _rectTransform.DOScale(_fullSize, scaleDuration)
                .SetUpdate(!stopAnimationWhenPaused)
                .SetEase(easeType);
        }

        public void ScaleOut()
        {
            if (!_isFullScaled) return;
            _isFullScaled = false;
            _rectTransform.localScale = _fullSize;
            _rectTransform.DOScale(_shrunkSize, scaleDuration)
                .SetUpdate(!stopAnimationWhenPaused)
                .SetEase(easeType);
        }
    }
}
