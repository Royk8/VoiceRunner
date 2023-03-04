using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if !UNITY_WEBGL || UNITY_EDITOR
    using UnityEngine.Windows.Speech;
#endif

namespace Project.Scripts.VoiceRecognition
{
    public class WindowsVoiceRecognizer : MonoBehaviour, IRecognizer
    {
        #region Singleton
            public static WindowsVoiceRecognizer Instance;
        
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
        
        private Dictionary<string, Action> _wordsToAction;
        #if !UNITY_WEBGL || UNITY_EDITOR
            private KeywordRecognizer _keywordRecognizer;

            private void WordRecognized(PhraseRecognizedEventArgs word)
            {
                _wordsToAction[word.text].Invoke();
            }
        #endif
        
        public void MapActions(Dictionary<string, Action> wordsToAction)
        {
        #if !UNITY_WEBGL || UNITY_EDITOR
            Debug.Log("Mapeando windows speech");
            _wordsToAction = wordsToAction;
            _keywordRecognizer = new KeywordRecognizer(wordsToAction.Keys.ToArray());
            _keywordRecognizer.OnPhraseRecognized += WordRecognized;
            _keywordRecognizer.Start();
        #endif
        }

        public void TurnOff()
        {
            #if !UNITY_WEBGL || UNITY_EDITOR
                _keywordRecognizer.Stop();
            #endif
        }

        public void TurnOn()
        {
            #if !UNITY_WEBGL || UNITY_EDITOR
                _keywordRecognizer.Start();
            #endif
        }
    }
}
