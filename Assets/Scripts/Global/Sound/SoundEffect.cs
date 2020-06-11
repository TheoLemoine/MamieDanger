using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Sound
{
    [Serializable]
    public class SoundEffect
    {
        [SerializeField] public string slug;
        [SerializeField] private AudioClip clip;
        [SerializeField] [Range(0f, 1f)] private float volume;
        [SerializeField] private bool loop;
        [SerializeField] private List<string> autoPlayOnScenes;
        
        private AudioSource _source;

        public void Play(bool restartIfPlaying = true)
        {
            if(restartIfPlaying || !_source.isPlaying) 
                _source.Play();
        }
        public void Pause() => _source.Pause();
        public void Stop() => _source.Stop();

        public void BindToManager(GameObject manager)
        {
            _source = manager.AddComponent<AudioSource>();

            _source.clip = clip;
            _source.volume = volume;
            _source.loop = loop;
            _source.playOnAwake = false;
        }

        public bool PlaysOnScene(Scene scene)
        {
            return autoPlayOnScenes.Contains(scene.name);
        }

        public float Volume
        {
            get => _source.volume;
            set => _source.volume = value;
        }

    }
}