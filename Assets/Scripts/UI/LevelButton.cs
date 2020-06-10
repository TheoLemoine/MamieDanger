using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] public string levelSceneName;
        [SerializeField] public bool locked;
        [SerializeField] private Material lockedMaterial;
        private RendererGroup _rendererGroup;

        public void Start()
        {
            _rendererGroup = GetComponent<RendererGroup>();
        }

        public void UpdateMaterials()
        {
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
