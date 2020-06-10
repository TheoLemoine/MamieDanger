using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private string levelSceneName;
        [SerializeField] private bool locked;
        [SerializeField] private Material lockedMaterial;
        private RendererGroup _rendererGroup;

        public void Start()
        {
            _rendererGroup = GetComponent<RendererGroup>();
            StartCoroutine(LateStart());
        }

        private IEnumerator LateStart()
        {
            yield return new WaitForFixedUpdate();
            if (locked)
                _rendererGroup.ChangeMaterial(lockedMaterial);
        }

        public void Click()
        {
            if (!locked)
                SceneManager.LoadScene(levelSceneName);
        }
    }
}
