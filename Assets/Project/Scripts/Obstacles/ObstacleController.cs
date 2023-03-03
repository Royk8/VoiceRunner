using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Obstacles
{
    public class ObstacleController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().LoseHealth();
            }
        }
    }
}
