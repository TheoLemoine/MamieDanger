using UI;
using UnityEngine;

namespace Utils
{
    public class ModelContainer : MonoBehaviour
    {
        [SerializeField] private GameObject model;
        private RendererGroup _rendererGroup;
    
        void Start()
        {
            _rendererGroup = GetComponent<RendererGroup>();
            _rendererGroup.UseParent(Instantiate(model, transform));
        }
    }
}
