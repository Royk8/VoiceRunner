using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Player
{
    public class PlayerDistanceCounter : MonoBehaviour
    {
        [SerializeField] private Text distanceText;
        private PlayerActions _actions;
        private float distance;

        private void Start()
        {
            _actions = GetComponent<PlayerActions>();
            StartCounting();
        }

        public void StartCounting()
        {
            distanceText.gameObject.SetActive(true);
            distance = 0;
        }

        private void Update()
        {
            distance += _actions.speed * Time.deltaTime;
            distanceText.text = $"{Mathf.Floor(distance)}m";
        }
    }
}
