using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Global.Input;
using Global.Save;
using UnityEngine;
using Utils;

namespace UI
{
    public class Carousel : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer uiLayer;
        [SerializeField] private LevelSelectorOrchestrator[] levels;
        [SerializeField] private int currentIndex;
        [SerializeField] private float radius = 15f;
        [SerializeField] private float angle = Mathf.PI / 9.5f;
        [SerializeField] private float transitionDuration = 0.6f;

        private float _animProgression;
        private int[] _goIds;
        private int _indexDiff;
        private Tween _tween;
        
        IEnumerator Start()
        {
            InputManager.PlayerRaycaster.AddListener(uiLayer.LayerIndex, Click);
            _goIds = new int[levels.Length];
            yield return new WaitForFixedUpdate();
            var levelsData = SaveManager.Instance.Data.levels;
            var index = 0;
            var locked = false;
            // Init layout of each level
            foreach (var level in levels)
            {
                var levelName = level.LevelButton.levelSceneName;
                var coinPickedNumber = levelsData.ContainsKey(levelName) ? levelsData[levelName].coinsPicked.Count : 0;
                level.LayoutLevelSelector(locked, coinPickedNumber);
                if (!levelsData.ContainsKey(levelName) || !levelsData[levelName].finished) locked = true;
                _goIds[index] = level.gameObject.GetInstanceID();
                index++;
            }
            CalcPos();
        }

        private void OnDestroy()
        {
            if (InputManager.IsReady)
                InputManager.PlayerRaycaster.RemoveListener(uiLayer.LayerIndex, Click);
        }

        private void Click(RaycastHit hit)
        {
            var goId = hit.collider.transform.parent.gameObject.GetInstanceID();
            var newIndex = Array.IndexOf(_goIds, goId);
            
            // If already on the clicked selector ( or close enough )
            if (newIndex == currentIndex || (newIndex == currentIndex - _indexDiff && Mathf.Abs(_indexDiff - _animProgression) < 0.25f))
            {
                levels[newIndex].LevelButton.Click();
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
                // Move each level in an arc-like shape
                var levelAngle = - Mathf.PI / 2f - angle * (index - currentIndex + _animProgression);
            
                level.transform.localPosition = new Vector3(
                    0f,
                    Mathf.Cos(levelAngle) * radius,
                    Mathf.Sin(levelAngle) * radius
                );
                
                // Create normalized value of how close each level is from being selected
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
                
                level.LevelSelectorTransition.UpdateTransition(progFactor);
                index++;
            }
        }

        public void UnlockAll()
        {
            // Cannot edit a dictionnary while iterating over it with a foreach
            var levelsCopy = new Dictionary<string, SaveData.LevelResults>(SaveManager.Instance.Data.levels);
            foreach (var keyValuePair in SaveManager.Instance.Data.levels)
            {
                var level = keyValuePair.Value;
                level.finished = true;
                levelsCopy[keyValuePair.Key] = level;
            }
            
            var index = 0;
            foreach (var levelSelector in levels)
            {
                levels[index].LayoutLevelSelector(false,
                    SaveManager.Instance.Data.levels.ContainsKey(levelSelector.LevelButton.levelSceneName)
                        ? SaveManager.Instance.Data.levels[levelSelector.LevelButton.levelSceneName].coinsPicked.Count
                        : 0
                    );
                index++;
            }

            SaveManager.Instance.Data.levels = levelsCopy;
            SaveManager.Instance.Persist();
        }
        
        public void ResetAll()
        {
            // Cannot edit a dictionnary while iterating over it with a foreach
            var levelsCopy = new Dictionary<string, SaveData.LevelResults>(SaveManager.Instance.Data.levels);
            foreach (var keyValuePair in SaveManager.Instance.Data.levels)
            {
                var level = keyValuePair.Value;
                level.finished = false;
                level.coinsPicked = new List<string>();
                levelsCopy[keyValuePair.Key] = level;
            }

            var index = 0;
            foreach (var levelSelector in levels)
            {
                levelSelector.LayoutLevelSelector(index != 0, 0);
                index++;
            }
            
            SaveManager.Instance.Data.levels = levelsCopy;
            SaveManager.Instance.Persist();
        }
    }
}
