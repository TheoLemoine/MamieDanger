using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class TimeMaterial : MonoBehaviour
    {
        [SerializeField] private string durationPropCode = "_duration";
        [SerializeField] private string startTimePropCode = "_startTime";
        [SerializeField] private bool doesResetTime;
        [SerializeField] private float resetTime;
        private Material _material;
        private float _duration;
        private int _startTimePropId;
    
        void Start()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            _material = meshRenderer != null ? meshRenderer.material : GetComponent<Image>().material;
        
            _startTimePropId = Shader.PropertyToID(startTimePropCode);
            _duration = _material.GetFloat(Shader.PropertyToID(durationPropCode));
            if (doesResetTime)
                SetTime(resetTime);
        }

        public void TriggerTime()
        {
            _material.SetFloat(_startTimePropId, Time.time);
        }

        public void SetTime(float time)
        {
            _material.SetFloat(_startTimePropId, time);
        }
    }
}
