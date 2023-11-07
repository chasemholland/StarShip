using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio manager
/// </summary>
public static class AudioManager
{
    #region Fields

    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioName, AudioClip> audioClips =
        new Dictionary<AudioName, AudioClip>();

    #endregion

    #region Properties

    /// <summary>
    /// Returns true if initialized, else false
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initailizes the audio manager
    /// </summary>
    /// <param name="source"></param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioName.ShipLaser, Resources.Load<AudioClip>("Ship_Laser"));
        audioClips.Add(AudioName.ShipLaserHit, Resources.Load<AudioClip>("Ship_Laser_Hit"));
        audioClips.Add(AudioName.Explosion, Resources.Load<AudioClip>("Explosion"));
        audioClips.Add(AudioName.Pickup, Resources.Load<AudioClip>("Pickup"));
        audioClips.Add(AudioName.Select, Resources.Load<AudioClip>("Select"));
    }

    /// <summary>
    /// Plays the provided audio clip
    /// </summary>
    /// <param name="clipName"></param>
    public static void Play(AudioName clipName)
    {
        if (clipName == AudioName.Explosion)
        {
            audioSource.PlayOneShot(audioClips[clipName]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips[clipName], 0.5f);
        }
        
    }

    #endregion
}
