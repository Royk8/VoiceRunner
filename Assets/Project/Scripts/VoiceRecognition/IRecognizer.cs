using System;
using System.Collections.Generic;

namespace Project.Scripts.VoiceRecognition
{
    public interface IRecognizer
    {
        public void MapActions(Dictionary<string, Action> wordsToAction);
    }
}