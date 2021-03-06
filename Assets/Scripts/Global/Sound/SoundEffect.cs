using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Global.Sound
{
    [Serializable]
    public class SoundEffect
    {
        [SerializeField] public string slug;
        [SerializeField] private AudioClip clip;
        [FormerlySerializedAs("volume")] [SerializeField] [Range(0f, 1f)] private float baseVolume;
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
            _source.volume = baseVolume;
            _source.loop = loop;
            _source.playOnAwake = false;
        }

        public bool PlaysOnScene(Scene scene)
        {
            return autoPlayOnScenes.Contains(scene.name);
        }

        private VolumeLevels _volume;
        public VolumeLevels Volume
        {
            get => _volume;
            set
            {
                float multiplier;

                switch (value)
                {
                    case VolumeLevels.Mute:
                        multiplier = 0f;
                        break;
                    case VolumeLevels.Low:
                        multiplier = 0.5f;
                        break;
                    case VolumeLevels.Loud:
                    default:
                        multiplier = 1f;
                        break;
                }
                
                _source.volume = baseVolume * multiplier;
                _volume = value;
            }
        }
    }
}