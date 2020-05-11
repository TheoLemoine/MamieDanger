using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Utils.Attributes;

namespace Utils
{
    public class OrthographicCameraUnZoom : MonoBehaviour
    {

        [TagSelector][SerializeField] private string playerTag;
        
        [SerializeField] private float orthographicSize;
        [SerializeField] private float transitionDuration;

        private CinemachineVirtualCamera _cam;
        private Tweener _currentTween;
        private float _baseOrthographicSize;

        private void Start()
        {
            var baseCam = Camera.main;

            if (baseCam == null)
                throw new Exception("You need an active camera in your scene");

            baseCam
                .GetComponent<CinemachineBrain>()
                .m_CameraActivatedEvent
                .AddListener(CameraActivatedCallback);
            
        }

        private void CameraActivatedCallback(ICinemachineCamera newCamera, ICinemachineCamera prevCamera)
        {
            if (!(newCamera is CinemachineVirtualCamera))
                throw new Exception("OrthographicCameraUnZoom can only be used with a cinemachine orthographic virtual camera.");

            _cam = newCamera as CinemachineVirtualCamera;

            if(!_cam.m_Lens.Orthographic)
                throw new Exception("OrthographicCameraUnZoom can only be used with a cinemachine orthographic virtual camera.");
            
            _baseOrthographicSize = _cam.m_Lens.OrthographicSize;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                _currentTween?.Kill();
                
                _currentTween = DOTween.To(
                    val => _cam.m_Lens.OrthographicSize = val, 
                    _cam.m_Lens.OrthographicSize, orthographicSize, 
                    transitionDuration
                );
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(playerTag))
            {
                _currentTween?.Kill();

                _currentTween = DOTween.To(
                    val => _cam.m_Lens.OrthographicSize = val, 
                    _cam.m_Lens.OrthographicSize, _baseOrthographicSize, 
                    transitionDuration
                );
            }
        }
    }
}