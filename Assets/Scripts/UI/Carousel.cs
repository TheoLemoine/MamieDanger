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
    [SerializeField] private float radius = 20f;
    [SerializeField] private float angle = Mathf.PI / 16;

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

        _tween = DOTween.To(value => _animProgression = value, 0f, _indexDiff, 0.6f);
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
            var scale = 1f;

            // Handle in animation
            if (_indexDiff != 0)
            {
                if (currentIndex - _indexDiff == index)
                    scale = 1f + _animProgression / _indexDiff * 0.5f;
                if (currentIndex == index)
                    scale = 1.5f - _animProgression / _indexDiff * 0.5f;
            }
            // If not in animation : max scale
            else if (index == currentIndex)
                scale = 1.5f;
            
            
            level.transform.localScale = Vector3.one * scale;
            
            index++;
        }
    }
}
