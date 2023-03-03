using System.Collections;
using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Scripts.Other
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private Text endGameText;
        [SerializeField] private PlayerDistanceCounter _distanceCounter;
        [SerializeField] private GameObject otherCanvas;

        public void EndGameToggle()
        {
            StartCoroutine(CountDistance(_distanceCounter.distance));
            otherCanvas.SetActive(false);
            StartCoroutine(ResetSceneCoroutine());
        }

        IEnumerator CountDistance(float target)
        {
            float time = 1f;
            float timePassed = 0;
            while (timePassed < time)
            {
                float delta = timePassed / time;
                endGameText.text = $"{Mathf.Floor(target * delta)}m";
                timePassed += Time.deltaTime;
                yield return null;
            }
            endGameText.text = $"{Mathf.Floor(target)}m";
        }

        IEnumerator ResetSceneCoroutine()
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene(0);
        }
    }
}