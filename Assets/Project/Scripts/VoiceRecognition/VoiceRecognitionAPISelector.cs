using System;
using System.Collections.Generic;

namespace Project.Scripts.VoiceRecognition
{
    public class VoiceRecognitionAPISelector : IRecognizer
    {
        public void MapActions(Dictionary<string, Action> wordsToAction)
        {
            #if !UNITY_WEBGL || UNITY_EDITOR
                        VoiceRecognizer.Instance.MapActions(wordsToAction);
            #endif

            #if UNITY_WEBGL && !UNITY_EDITOR
                            WebVoiceRecognizer.Instance.MapActions(wordsToAction);
            #endif
        }
    }
}