using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Obstacles
{
    public class ObstacleGenerator : MonoBehaviour
    {
        [SerializeField] private Transform left, right, center;
        [SerializeField] private GameObject[] onePointObstacle;
        [SerializeField] private GameObject[] fullRowObstacles;

        private void Start()
        {
            GenerateRandom();
        }

        private void GenerateRandom()
        {
            int rnd = Random.Range(0, 2);
            GameObject obs = onePointObstacle[Random.Range(0, onePointObstacle.Length)];
            switch (rnd)
            {
                case 0:
                    Instantiate(obs, left);
                    break;
                case 1:
                    if (Random.Range(0,2) == 0)
                    {
                        obs = fullRowObstacles[Random.Range(0, fullRowObstacles.Length)];
                    }
                    Instantiate(obs, center);
                    break;
                case 2:
                    obs = onePointObstacle[Random.Range(0, onePointObstacle.Length)];
                    Instantiate(obs, right);
                    break;
                default:
                    break;
            }
        }

        public void Regenerate()
        {
            Clear();
            GenerateRandom();
        }

        private void Clear()
        {
            foreach (Transform child in left)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in center)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in right)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
