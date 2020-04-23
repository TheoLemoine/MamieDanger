using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global
{
    public class Preload : MonoBehaviour
    {
        
        // cant be 0 at start
        private static int _goBackTo = -1;
        
        private void Start()
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            if (_goBackTo == -1)
            {
                var sceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(sceneIndex + 1);
            }
            
            SceneManager.LoadScene(_goBackTo);
        }

#if UNITY_EDITOR
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitLoadingScene()
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex != 0)
            {
                _goBackTo = sceneIndex;
                SceneManager.LoadScene(0);
            }
        }
        
#endif
        
    }
}