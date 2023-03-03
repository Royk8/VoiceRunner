using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if !UNITY_WEBGL || UNITY_EDITOR
    using UnityEngine.Windows.Speech;
#endif

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
        

        private Dictionary<string, Action> wordsToAction;
        #if !UNITY_WEBGL || UNITY_EDITOR
            private KeywordRecognizer keywordRecognizer;

            private void WordRecognized(PhraseRecognizedEventArgs word)
            {
                Debug.Log($"{word.confidence}, {word.text}");
                wordsToAction[word.text].Invoke();
            }
        #endif
        
        public void MapActions(Dictionary<string, Action> wordsToAction)
        {
        #if !UNITY_WEBGL || UNITY_EDITOR
            this.wordsToAction = wordsToAction;
            keywordRecognizer = new KeywordRecognizer(wordsToAction.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += WordRecognized;
            keywordRecognizer.Start();
        #endif
        }

    }
}
