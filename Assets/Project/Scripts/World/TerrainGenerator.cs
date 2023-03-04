using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.World
{
    public class TerrainGenerator : MonoBehaviour
    {
        #region Singleton

        public static TerrainGenerator Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion

        [SerializeField] private GameObject terrainTile;
        [SerializeField] private int poolSize;

        public Queue<GameObject> tilePool;

        private void Start()
        {
            tilePool = new Queue<GameObject>();
            TerrainTile firstTile = FindObjectOfType<TerrainTile>();
            tilePool.Enqueue(firstTile.gameObject);
            GameObject tile = Instantiate(terrainTile, Vector3.forward * 50, Quaternion.identity);
            tilePool.Enqueue(tile);
        }

        public void SpawnTerrain(Vector3 spawmerPosition)
        {
            if (tilePool.Count >= poolSize)
            {
                GameObject oldestTile = tilePool.Dequeue();
                oldestTile.transform.position = spawmerPosition + Vector3.forward * 100;
                oldestTile.GetComponent<TerrainTile>().RegenerateObstacles();
                tilePool.Enqueue(oldestTile);
            }
            else
            {
                GameObject tile = Instantiate(terrainTile, spawmerPosition + Vector3.forward * 100, Quaternion.identity);
                tilePool.Enqueue(tile);
            }
        }
    }
}