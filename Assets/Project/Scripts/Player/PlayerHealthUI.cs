using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Player
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] hearts;
        [SerializeField] private Image damaged;

        public void LoseOneHealth(int current)
        {
            hearts[current].SetActive(false);
            StartCoroutine(GetDamagetCoroutine());
        }

        public void Reset()
        {
            foreach (GameObject heart in hearts)
            {
                heart.SetActive(true);
            }
        }

        IEnumerator GetDamagetCoroutine()
        {
            float duration = 1f;
            float timepassed = 0;
            while (timepassed < duration)
            {
                Color color = damaged.color;
                color.a = 1 - timepassed / duration;
                damaged.color = color;
                timepassed += Time.deltaTime;
                yield return null;
            }
            damaged.color = new Color(1,0,0,0);
        }
    }
}
