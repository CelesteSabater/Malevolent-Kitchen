using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Systems.Audio
{
    [Serializable]
    public enum NPCSentenceType
    {
        Strike,
        CompleteFood,
        BurnFood,
        RecipeStep,
        NewRecipe,
        Annoy,
        ExtraCuts
    }

    [Serializable]
    public struct Sentence
    {
        public NPCSentenceType type;
        public string sentence;
    }

    public class NPCSpeaker : MonoBehaviour
    {
        [SerializeField] private Sentence[] _sentences;
        private Queue _sentenceQueue = new Queue();
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }

        private void Start() 
        {
            GameEvents.current.onStrike += OnStrike;
            GameEvents.current.onCompleteFood += OnCompleteFood;
            GameEvents.current.onBurnFood += OnBurnFood;
            GameEvents.current.onRecipeStep += OnRecipeStep;
            GameEvents.current.onNewRecipe += OnNewRecipe;
            GameEvents.current.onAnnoy += OnAnnoy;
            GameEvents.current.onExtraCuts += OnExtraCuts;
        }

        private void OnDestroy() 
        {
            GameEvents.current.onStrike -= OnStrike;
            GameEvents.current.onCompleteFood -= OnCompleteFood;
            GameEvents.current.onBurnFood -= OnBurnFood;
            GameEvents.current.onRecipeStep -= OnRecipeStep;
            GameEvents.current.onNewRecipe -= OnNewRecipe;
            GameEvents.current.onAnnoy -= OnAnnoy;
            GameEvents.current.onExtraCuts -= OnExtraCuts;
        }
        
        void Update()
        {
            Speak();
        }

        private void Speak()
        {
            if (_audioSource.isPlaying || _sentenceQueue.Count == 0)
                return;

            string sentence = _sentenceQueue.Dequeue() as string;
            AudioSystem.Instance.PlaySentence(sentence, _audioSource);
        }

        private string RandomSentence(NPCSentenceType sentenceType)
        {
            Sentence[] matchingSentences = Array.FindAll(_sentences, s => s.type == sentenceType);

            if (matchingSentences.Length == 0)
                return string.Empty;

            int randomIndex = UnityEngine.Random.Range(0, matchingSentences.Length);
            return matchingSentences[randomIndex].sentence;
        }

        private void OnStrike() => _sentenceQueue.Enqueue(RandomSentence(NPCSentenceType.Strike));
        private void OnCompleteFood() => _sentenceQueue.Enqueue(RandomSentence(NPCSentenceType.CompleteFood));
        private void OnBurnFood(string stationGuid) => _sentenceQueue.Enqueue(RandomSentence(NPCSentenceType.BurnFood));
        private void OnRecipeStep(string stationGuid) => _sentenceQueue.Enqueue(RandomSentence(NPCSentenceType.RecipeStep));
        private void OnNewRecipe(string recipeName) => _sentenceQueue.Enqueue(recipeName);
        private void OnAnnoy(float pct)
        {
            if (pct <= UnityEngine.Random.Range(0, 100))
                _sentenceQueue.Enqueue(RandomSentence(NPCSentenceType.Annoy));
        }
        private void OnExtraCuts() => _sentenceQueue.Enqueue(RandomSentence(NPCSentenceType.ExtraCuts));
    }
}
