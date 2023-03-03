using UnityEngine;

namespace Project.Scripts.Sound
{
    public class SoundCollection : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _clips;

        public AudioClip GetRandom()
        {
            int rnd = Random.Range(0, _clips.Length);
            return _clips[rnd];
        }
    }
}
