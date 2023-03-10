using System;
using UnityEngine;

namespace Project.Scripts.Player
{
    [RequireComponent(typeof(PlayerActions))]
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private PlayerActions _actions;
        void Start()
        {
            _actions = GetComponent<PlayerActions>();
            _actions.OnBark += Bark;
            _actions.OnHurt += Hurt;
            _actions.OnDead += Die;
        }

        private void Die()
        {
            _animator.SetBool("Dead", true);
        }

        private void Hurt()
        {
            _animator.SetTrigger("Hurt");
        }

        private void Bark()
        {
            _animator.SetTrigger("Bark");
        }

        public void SetState(int state)
        {
            _animator.SetInteger("State", state);
        }

        private void OnDisable()
        {
            _actions.OnBark -= Bark;
            _actions.OnHurt -= Hurt;
            _actions.OnDead -= Die;
        }
    }
}
