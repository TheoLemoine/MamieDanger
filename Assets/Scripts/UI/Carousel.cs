using System;
using DG.Tweening;
using Global.Input;
using UnityEngine;
using Utils;

public class Carousel : MonoBehaviour
{
    [SerializeField] private SingleUnityLayer uiLayer;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private int currentIndex;
    [SerializeField] private float radius = 15f;
    [SerializeField] private float angle = Mathf.PI / 9.5f;
    [SerializeField] private float transitionDuration = 0.6f;
    [SerializeField] private float defaultScale = 1f;
    [SerializeField] private float frontScale = 1.5f;

    private float _animProgression;
    private int _indexDiff;
    private Tween _tween;
    
    void Start()
    {
        InputManager.PlayerRaycaster.AddListener(uiLayer.LayerIndex, Click);
        CalcPos();
    }

    private void Click(RaycastHit hit)
    {
        var go = hit.collider.transform.parent.gameObject;
        var newIndex = Array.IndexOf(levels, go);
        if (newIndex == currentIndex || (newIndex == currentIndex - _indexDiff && Mathf.Abs(_indexDiff - _animProgression) < 0.25f))
        {
            go.GetComponent<LevelButton>().Click();
            return;
        }

        // Complete animation if not already
        if (_tween != null && !_tween.IsComplete())
            _tween.Complete();
        
        _indexDiff = currentIndex - newIndex;

        _tween = DOTween.To(value => _animProgression = value, 0f, _indexDiff, transitionDuration);
        _tween.onUpdate = CalcPos;
        _tween.onComplete = () =>
        {
            currentIndex = newIndex;
            _animProgression = 0f;
            _indexDiff = 0;
            CalcPos();
        };
    }

    private void CalcPos()
    {
        var index = 0;
        foreach (var level in levels)
        {
            var levelAngle = - Mathf.PI / 2f - angle * (index - currentIndex + _animProgression);
            
            level.transform.localPosition = new Vector3(
                0f,
                Mathf.Cos(levelAngle) * radius,
                Mathf.Sin(levelAngle) * radius
            );

            // Init to default scale
            var scale = defaultScale;
            var diffScale = frontScale - defaultScale;

            // Handle in animation
            if (_indexDiff != 0)
            {
                if (currentIndex - _indexDiff == index)
                    scale = defaultScale + _animProgression / _indexDiff * diffScale;
                if (currentIndex == index)
                    scale = frontScale - _animProgression / _indexDiff * diffScale;
            }
            // If not in animation : max scale
            else if (index == currentIndex)
                scale = frontScale;
            
            
            level.transform.localScale = Vector3.one * scale;
            
            index++;
        }
    }
}
