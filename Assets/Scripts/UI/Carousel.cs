using System;
using DG.Tweening;
using Global.Input;
using UnityEngine;
using Utils;

public class Carousel : MonoBehaviour
{
    [SerializeField] private SingleUnityLayer uiLayer;
    [SerializeField] private LevelSelectorTransition[] levels;
    [SerializeField] private int currentIndex;
    [SerializeField] private float radius = 15f;
    [SerializeField] private float angle = Mathf.PI / 9.5f;
    [SerializeField] private float transitionDuration = 0.6f;

    private float _animProgression;
    private int[] _goIds;
    private int _indexDiff;
    private Tween _tween;
    
    void Start()
    {
        InputManager.PlayerRaycaster.AddListener(uiLayer.LayerIndex, Click);
        _goIds = new int[levels.Length];
        var index = 0;
        foreach (var level in levels)
        {
            _goIds[index] = level.gameObject.GetInstanceID();
            index++;
        }
        CalcPos();
    }
    
    private void Click(RaycastHit hit)
    {
        var goId = hit.collider.transform.parent.gameObject.GetInstanceID();
        var newIndex = Array.IndexOf(_goIds, goId);
        if (newIndex == currentIndex || (newIndex == currentIndex - _indexDiff && Mathf.Abs(_indexDiff - _animProgression) < 0.25f))
        {
            levels[newIndex].GetComponent<LevelButton>().Click();
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
            
            var progFactor = 0f;
            if (_indexDiff != 0)
            {
                if (currentIndex - _indexDiff == index)
                    progFactor = _animProgression / _indexDiff;
                if (currentIndex == index)
                    progFactor = 1f - _animProgression / _indexDiff;
            }
            else if (index == currentIndex)
                progFactor = 1f;
            
            level.UpdateTransition(progFactor);
            index++;
        }
    }
}
