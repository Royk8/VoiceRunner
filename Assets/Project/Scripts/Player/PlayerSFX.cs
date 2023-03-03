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
        [SerializeField] private SoundCollection hurt;
        private AudioSource _audioSource;
        private PlayerActions _actions;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _actions = GetComponent<PlayerActions>();
            _actions.OnBark += Bark;
            _actions.OnHurt += Hurt;
        }

        public void Bark()
        {
            _audioSource.PlayOneShot(barks.GetRandom());
        }

        public void Hurt()
        {
            _audioSource.PlayOneShot(hurt.GetRandom());
        }
        
        private void OnDisable()
        {
            _actions.OnBark -= Bark;
        }
    }
}
