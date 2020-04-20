using UnityEngine;

namespace Granny
{
    public class GrannyBlockingArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent<GrannyController>(out var grannyController))
            {
                grannyController.StopInteraction();
            }  
        }
    }
}
