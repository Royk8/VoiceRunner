using System;
using System.Collections.Generic;

namespace Project.Scripts.VoiceRecognition
{
    public class VoiceRecognitionAPISelector : IRecognizer
    {
        public void MapActions(Dictionary<string, Action> wordsToAction)
        {
            #if !UNITY_WEBGL || UNITY_EDITOR
                        WindowsVoiceRecognizer.Instance.MapActions(wordsToAction);
            #endif

            #if UNITY_WEBGL && !UNITY_EDITOR
                            WebVoiceRecognizer.Instance.MapActions(wordsToAction);
            #endif
        }

        public void TurnOff()
        {
            WindowsVoiceRecognizer.Instance.TurnOff();
            WebVoiceRecognizer.Instance.TurnOff();
        }

        public void TurnOn()
        {
            WindowsVoiceRecognizer.Instance.TurnOn();
            WebVoiceRecognizer.Instance.TurnOn();
        }
    }
}