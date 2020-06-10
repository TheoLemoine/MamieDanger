using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Sound
{
    /**
     * Weird flex : All class use the first instantiated class data...
     * this allow for event calls via prefab binding.
     */
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<SoundEffect> sounds;
        private static SoundManager _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else return;
            
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoad;

            foreach (var sound in sounds)
            {
                sound.BindToManager(gameObject);
            }
        }

        private void OnSceneLoad(Scene newScene, LoadSceneMode mode)
        {
            foreach (var sound in _instance.sounds) 
            {
                if (sound.PlaysOnScene(newScene)) sound.Play();
                else sound.Stop();
            }
        }

        private SoundEffect GetSound(string slug)
        {
            try
            {
                return _instance.sounds.Find(sound => sound.slug == slug);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public void Play(string slug) => GetSound(slug)?.Play();
        public void Pause(string slug) => GetSound(slug)?.Play();
        public void Stop(string slug) => GetSound(slug)?.Stop();
    }
}