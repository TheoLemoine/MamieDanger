using DG.Tweening;
using UnityEngine;

public class UISlide : MonoBehaviour
{

    [SerializeField] private Vector2 offsetWhenVisible;
    [SerializeField] private Vector2 offsetWhenNotVisible;
    [SerializeField] private bool startVisible;
    [SerializeField] private bool animateAtStart;

    private bool _isVisible;
    private RectTransform _rectTransform;
    private Vector3 _startPosition;
    
    void Start()
    {
        _isVisible = startVisible;
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
    
    void SlideIn()
    {
        if (_isVisible) return;
        _isVisible = true;
        _rectTransform.localPosition = _startPosition + new Vector3(offsetWhenNotVisible.x, offsetWhenNotVisible.y, 0f);
        var localEndPos = _startPosition + new Vector3(offsetWhenVisible.x, offsetWhenVisible.y, 0f);
        _rectTransform.DOLocalMove(localEndPos, 1f);
    }

    void SlideOut()
    {
        if (_isVisible) return;
        _isVisible = true;
        _rectTransform.localPosition = _startPosition + new Vector3(offsetWhenVisible.x, offsetWhenVisible.y, 0f);
        var localEndPos = _startPosition + new Vector3(offsetWhenNotVisible.x, offsetWhenNotVisible.y, 0f);
        _rectTransform.DOLocalMove(localEndPos, 1f);
    }
}
