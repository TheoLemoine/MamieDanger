using System;
using System.Collections;
using Car;
using UnityEngine;

namespace Utils
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float period = 1f;
        [SerializeField] private bool overrideCarSpeed;
        [SerializeField] private float carSpeed;

        private Transform _transform;
        private void Start()
        {
            _transform = GetComponent<Transform>();
            
            StartCoroutine(InstantiatePrefabs());
        }

        private IEnumerator InstantiatePrefabs()
        {
            for (;;)
            {
                var instantiated = Instantiate(prefab, _transform);
                if (overrideCarSpeed)
                    instantiated.GetComponent<CarController>().TargetSpeed = carSpeed;
                yield return new WaitForSeconds(period);
            }
        }
    }
}