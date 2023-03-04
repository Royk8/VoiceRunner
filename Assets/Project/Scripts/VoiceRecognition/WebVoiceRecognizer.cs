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
            private static extern void StartRecognizer();
            [DllImport("__Internal")]
            private static extern void StopRecognizer();
            [DllImport("__Internal")]
            private static extern void SetUPRecognizer(string words);
        #endif

        private void RecognizerStart()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                SetUPRecognizer("Hola,Salta,Derecha,Izquierda,Jump,Left,Right,Hello,Hi");
            #endif
        }

        public void MapActions(Dictionary<string, Action> wordsToAction)
        {
            Debug.Log("Mapeando web api");
            _wordsToAction = wordsToAction;
            RecognizerStart();
        }

        public void TurnOff()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                StopRecognizer();
            #endif
        }

        public void TurnOn()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
                StartRecognizer();
            #endif
        }

        public void OnFinalResult(string word)
        {
            if (_wordsToAction.ContainsKey(word))
            {
                _wordsToAction[word].Invoke();
            }
        }

    }
}