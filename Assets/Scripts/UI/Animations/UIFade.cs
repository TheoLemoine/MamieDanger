using DG.Tweening;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    [SerializeField] [Range(0.2f, 2f)] float fadeDuration = 1f;
    [SerializeField] private Ease easeType = Ease.OutQuad;
    [SerializeField] private bool stopAnimationWhenPaused;
    [SerializeField] private bool startVisible;
    private CanvasGroup _canvasGroup;
    private bool _visible;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _visible = startVisible;
        _canvasGroup.alpha = _visible ? 1f : 0f;
        _canvasGroup.interactable = _visible;
        _canvasGroup.blocksRaycasts = _visible;
    }

    public void FadeIn()
    {
        _visible = true;
        _canvasGroup.DOFade(1f, fadeDuration)
            .SetUpdate(!stopAnimationWhenPaused)
            .SetEase(easeType);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void FadeOut()
    {
        _visible = false;
        _canvasGroup.DOFade(0f, fadeDuration)
            .SetUpdate(!stopAnimationWhenPaused)
            .SetEase(easeType);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void ToggleFade()
    {
        if (_visible)
            FadeOut();
        else
            FadeIn();
    }
}
