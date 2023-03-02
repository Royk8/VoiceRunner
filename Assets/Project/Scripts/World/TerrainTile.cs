using UnityEngine;

namespace Project.Scripts.World
{
    public class TerrainTile : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TerrainGenerator.Instance.SpawnTerrain(transform.position);
            }
        }
    }
}
