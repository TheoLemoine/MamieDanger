using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPauseEvent;
        [SerializeField] private UnityEvent onResumeToGameEvent;
        [SerializeField] private UnityEvent onResumeToASceneEvent;
      
        public void PauseGame()
        {
            StopTime();
            onPauseEvent.Invoke();
        }
    
        public void ResumeToGame()
        {
            ResumeTime();
            onResumeToGameEvent.Invoke();
        }
    
        public void ResumeToAScene()
        {
            ResumeTime();
            onResumeToASceneEvent.Invoke();
        }

        private void StopTime()
        {
            Time.timeScale = 0;
        }

        private void ResumeTime()
        {
            Time.timeScale = 1;
        }
    }
}
