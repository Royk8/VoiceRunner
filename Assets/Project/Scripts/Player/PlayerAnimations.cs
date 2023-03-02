using UnityEngine;

namespace Project.Scripts.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;
        void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
    }
}
