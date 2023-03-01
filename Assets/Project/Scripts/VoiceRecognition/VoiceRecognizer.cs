using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognizer : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;

    private Dictionary<string, Action> wordsToAction;
    [SerializeField] private GameObject thing;
    void Start()
    {
        wordsToAction = new Dictionary<string, Action>();
        
        wordsToAction.Add("Hola", HolaMundo);
        keywordRecognizer = new KeywordRecognizer(wordsToAction.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += WordRecognized;
        keywordRecognizer.Start();
    }

    private void WordRecognized(PhraseRecognizedEventArgs word)
    {
        Debug.Log($"{word.confidence}, {word.text}");
        wordsToAction[word.text].Invoke();
    }

    private void HolaMundo()
    {
        thing.SetActive(!thing.activeSelf);
    }

}
