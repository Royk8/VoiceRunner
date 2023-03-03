using Project.Scripts.Obstacles;
using UnityEngine;

namespace Project.Scripts.World
{
    public class TerrainTile : MonoBehaviour
    {

        [SerializeField] private ObstacleGenerator[] obstacles;

        public void RegenerateObstacles()
        {
            foreach (ObstacleGenerator obstacle in obstacles)
            {
                obstacle.Regenerate();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TerrainGenerator.Instance.SpawnTerrain(transform.position);
            }
        }
    }
}
