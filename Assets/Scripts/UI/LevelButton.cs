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
        [SerializeField] private RendererGroup rendererGroup;

        public void UpdateMaterials()
        {
            if (locked)
                rendererGroup.ChangeMaterial(lockedMaterial);
        }

        public void Click()
        {
            if (!locked)
                SceneManager.LoadScene(levelSceneName);
        }
    }
}
