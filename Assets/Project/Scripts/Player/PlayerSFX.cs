using System;
using Project.Scripts.Sound;
using UnityEngine;

namespace Project.Scripts.Player
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(PlayerActions))]
    public class PlayerSFX : MonoBehaviour
    {
        [SerializeField] private SoundCollection barks;
        private AudioSource _audioSource;
        private PlayerActions _actions;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _actions = GetComponent<PlayerActions>();
            _actions.OnBark += Bark;
        }

        public void Bark()
        {
            _audioSource.PlayOneShot(barks.GetRandom());
        }
        
        private void OnDisable()
        {
            _actions.OnBark -= Bark;
        }
    }
}
