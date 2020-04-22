using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    [SerializeField] [Range(0.2f, 2f)] float fadeDuration = 1f;
    private CanvasGroup _canvasGroup;
    private bool _visible;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _visible = _canvasGroup.alpha > 0f;
        _canvasGroup.interactable = _visible;
    }

    public void FadeIn()
    {
        _visible = true;
        _canvasGroup.DOFade(1f, fadeDuration);
        _canvasGroup.interactable = true;
    }

    public void FadeOut()
    {
        _visible = false;
        _canvasGroup.DOFade(0f, fadeDuration);
        _canvasGroup.interactable = false;
    }

    public void ToggleFade()
    {
        if (_visible)
            FadeOut();
        else
            FadeIn();
    }
}
