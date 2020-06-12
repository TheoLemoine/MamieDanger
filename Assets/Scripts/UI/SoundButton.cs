using System;
using Global.Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class VolumeChangeEvent : UnityEvent<VolumeLevels>
    {
    }

    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private Image volumeIcon;
        [SerializeField] private Sprite[] volumeSprites;
        [SerializeField] private VolumeChangeEvent volumeEvent;
        private VolumeLevels[] _soundLevelArray;
        private int _volumeIndex;

        private void Start()
        {
            _soundLevelArray = new[] {
                VolumeLevels.Loud,
                VolumeLevels.Mute,
                VolumeLevels.Low,
            };
            _volumeIndex = Array.IndexOf(_soundLevelArray, SoundManager.GlobalVolume);
            volumeIcon.sprite = volumeSprites[_volumeIndex];
            volumeEvent.Invoke(_soundLevelArray[_volumeIndex]);
        }

        public void ChangeSoundLevel()
        {
            _volumeIndex = (_volumeIndex + 1) % _soundLevelArray.Length;
            volumeIcon.sprite = volumeSprites[_volumeIndex];
            volumeEvent.Invoke(_soundLevelArray[_volumeIndex]);
        }
    }
}