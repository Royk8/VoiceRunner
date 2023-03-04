using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Player
{
    public class PlayerDistanceCounter : MonoBehaviour
    {
        [SerializeField] private GameObject distancePanel;
        [SerializeField] private Text distanceText;
        private PlayerActions _actions;
        public float distance { get; private set; }
        
        public void OnEnable()
        {
            _actions = GetComponent<PlayerActions>();
            distancePanel.SetActive(true);
            distanceText.gameObject.SetActive(true);
            distance = 0;
        }

        private void Update()
        {
            distance += _actions.runSpeed * Time.deltaTime;
            distanceText.text = $"{Mathf.Floor(distance)}m";
        }
    }
}
