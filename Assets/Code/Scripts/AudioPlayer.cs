using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Algoritcom.TechnicalTest.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip _soundEffect;
        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource.clip = _soundEffect;
        }

        public void PlaySoundEffect()
        {
            _audioSource.Play();
        }
    }
}


