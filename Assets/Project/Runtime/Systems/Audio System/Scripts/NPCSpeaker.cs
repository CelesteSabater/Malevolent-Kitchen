using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Systems.Audio
{
    public class NPCSpeaker : MonoBehaviour
    {
        private Queue _sentenceQueue = new Queue();
        private AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponentInChildren<AudioSource>();
        }
        
        void Update()
        {
            Speak();
        }

        private void Speak()
        {
            if (_audioSource.isPlaying || _sentenceQueue.Count == 0)
                return;

            SpeakNextSentence();
        }

        private void SpeakNextSentence()
        {
            if (_sentenceQueue.Count == 0)
                return;

            string sentence = _sentenceQueue.Dequeue() as string;
            AudioSystem.Instance.PlaySentence(sentence, _audioSource);
        }
    }
}
