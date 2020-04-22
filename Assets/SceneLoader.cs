using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneLoader : MonoBehaviour
{
    public void GoTo(string sceneName)
    {
        UnitySceneManager.LoadScene(sceneName);
    }
}
