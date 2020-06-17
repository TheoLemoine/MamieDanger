using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] public string levelSceneName;
        [SerializeField] public bool locked;
        [SerializeField] private Material lockedMaterial;
        [SerializeField] private RendererGroup rendererGroup;
        [SerializeField] private float bounceFactor = 1.15f;
        [SerializeField] private float lockedBounceFactor = 1.05f;
        [SerializeField] private float bounceDuration = 0.15f;
        [SerializeField] private float lockedBounceDuration = 0.1f;
        private float _currentBounceFactor = 1f;
        private float _lastBounceFactor = 1f;
        private bool _bounceIsFinished;
        private Tweener _tween;
        private Material _defaultMaterial;

        public void UpdateMaterials()
        {
            if (locked)
                rendererGroup.ChangeMaterial(lockedMaterial);
            else
                rendererGroup.ResetMaterial();
        }

        public void Click()
        {
            _bounceIsFinished = false;
            // Change tween params depending on level state
            var usedBounceFactor = lockedBounceFactor;
            var duration = lockedBounceDuration;
            if (!locked)
            {
                usedBounceFactor = bounceFactor;
                duration = bounceDuration;
                StartCoroutine(AsyncLoadScene());
            }
            
            if (_tween != null && !_tween.IsComplete()) _tween.Kill();
            // Bounce is made of two tweens
            _tween = DOTween.To(value => _currentBounceFactor = value, 1f, usedBounceFactor, duration);
            _tween.SetEase(Ease.OutSine);
            _tween.onUpdate = UpdateScale;
            _tween.onComplete = () =>
            {
                _tween = DOTween.To(value => _currentBounceFactor = value, usedBounceFactor, 1f, duration);
                _tween.SetEase(Ease.InSine);
                _tween.onUpdate = UpdateScale;
                _tween.onComplete = UpdateScale;
                _bounceIsFinished = true;
            };
        }

        private IEnumerator AsyncLoadScene()
        {
            var asyncLoad = SceneManager.LoadSceneAsync(levelSceneName);
            
            while (!asyncLoad.isDone || !_bounceIsFinished)
            {
                yield return null;
            }
        }

        private void UpdateScale()
        {
            // Scale only by the difference between last and current scale
            transform.localScale *= _currentBounceFactor / _lastBounceFactor;
            _lastBounceFactor = _currentBounceFactor;
        }
    }
}
