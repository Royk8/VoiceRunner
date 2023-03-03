using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = System.Object;

namespace Project.Scripts.VoiceRecognition
{
    public class WebVoiceRecognizer : MonoBehaviour, IRecognizer
    {
        #region Singleton
            public static WebVoiceRecognizer Instance;
        
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
        #if UNITY_WEBGL && !UNITY_EDITOR
            [DllImport("__Internal")]
            private static extern void SetUPRecognizer(string words);
        #endif

        private void StartRecognizer()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                SetUPRecognizer("Hola,Salta,Derecha,Izquierda,Jump,Left,Right");
            #endif
        }

        public void MapActions(Dictionary<string, Action> wordsToAction)
        {
            
            _wordsToAction = wordsToAction;
            StartRecognizer();
        }

        public void OnFinalResult(string word)
        {
            string w = word.Remove(word.Length - 1, 1);
            if (_wordsToAction.ContainsKey(w))
            {
                _wordsToAction[w].Invoke();
            }
        }

    }
}