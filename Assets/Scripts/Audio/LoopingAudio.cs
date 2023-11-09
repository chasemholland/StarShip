using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class LoopingAudio : MonoBehaviour
{
    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
    {
        // make sure there is only one audio source game object
        if (!LoopingAudioManager.Initialized)
        {
            // initialize audio manager and persist throughout game
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            LoopingAudioManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
            LoopingAudioManager.Switch(AudioName.Ambient);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
