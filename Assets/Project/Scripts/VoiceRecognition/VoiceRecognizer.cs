using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Project.Scripts.VoiceRecognition
{
    public class VoiceRecognizer : MonoBehaviour, IRecognizer
    {
        #region Singleton
            public static VoiceRecognizer Instance;
        
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
        
        private KeywordRecognizer keywordRecognizer;
        private Dictionary<string, Action> wordsToAction;

        private void WordRecognized(PhraseRecognizedEventArgs word)
        {
            Debug.Log($"{word.confidence}, {word.text}");
            wordsToAction[word.text].Invoke();
        }
        public void MapActions(Dictionary<string, Action> wordsToAction)
        {
            this.wordsToAction = wordsToAction;
            keywordRecognizer = new KeywordRecognizer(wordsToAction.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += WordRecognized;
            keywordRecognizer.Start();
        }
    }
}
