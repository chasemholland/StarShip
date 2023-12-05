using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class SoundSettings : MonoBehaviour
{
    #region Fields
    AudioSource music;
    AudioSource effects;

    [SerializeField]
    Sprite MuteMusic_Button;

    [SerializeField]
    Sprite MuteMusic_Button_Highlighted;

    [SerializeField]
    Sprite UnMuteMusic_Button;

    [SerializeField]
    Sprite UnMuteMusic_Button_Highlighted;

    [SerializeField]
    Sprite MuteEffects_Button;

    [SerializeField]
    Sprite MuteEffects_Button_Highlighted;

    [SerializeField]
    Sprite UnMuteEffects_Button;

    [SerializeField]
    Sprite UnMuteEffects_Button_Highlighted;

    [SerializeField]
    Button MuteMusicButton;

    [SerializeField]
    Button MuteEffectsButton;

    [SerializeField]
    Slider MusicVolumeSlider;

    [SerializeField]
    Slider EffectsVolumeSlider;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // check if music is muted
        music = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        if(music.volume == 0)
        {
            // switch sprite to unmute music
            Image muteMusic = GameObject.FindWithTag("MuteMusic").GetComponent<Image>();
            muteMusic.sprite = UnMuteMusic_Button;
        }

        // set music slider value
        MusicVolumeSlider.value = music.volume;

        // check if effects are muted
        effects = GameObject.Find("GameAudioSource").GetComponent<AudioSource>();
        if(effects.volume == 0)
        {
            // switch sprite to unmute music
            Image muteEffects = GameObject.FindWithTag("MuteEffects").GetComponent<Image>();
            muteEffects.sprite = UnMuteEffects_Button;
        }

        // set effects volume slider
        EffectsVolumeSlider.value = effects.volume;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    #endregion


    #region Public Methods

    /// <summary>
    /// Handles exit button click
    /// </summary>
    public void HandleExitButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("MainMenu");

        // load main menu
        //SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Handle mute music button
    /// </summary>
    public void HandleMuteMusicButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // get reference image
        Image muteMusicImage = GameObject.FindWithTag("MuteMusic").GetComponent<Image>();

        // check which button is displayed and switch
        if (muteMusicImage.sprite == MuteMusic_Button)
        {
            // switch the sprite, highlighted, pressed, and selected
            UnMuteMusicSpriteChange(muteMusicImage);

            // change music volume
            music.volume = 0;

            // set music slider value
            MusicVolumeSlider.value = music.volume;
        }
        else
        {
            // switch the sprite, highlighted, pressed, and selected
            MuteMusicSpriteChange(muteMusicImage);

            // change music volume to half
            music.volume = 0.5f;

            // set music slider value
            MusicVolumeSlider.value = music.volume;
        }

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);
    }

    /// <summary>
    /// Handles music slider value change
    /// </summary>
    public void HandleMusicSliderOnValueChangedEvent()
    {
        // get reference image
        Image muteMusicImage = GameObject.FindWithTag("MuteMusic").GetComponent<Image>();

        // check which sprite is active on volume slider change
        // if was muted and now not muted
        if (MusicVolumeSlider.value > 0 && muteMusicImage.sprite == UnMuteMusic_Button)
        {
            // switch the sprite, highlighted, pressed, and selected
            MuteMusicSpriteChange(muteMusicImage);

            // change music volume
            music.volume = MusicVolumeSlider.value;
        }
        // if not muted and volume changed
        else if (MusicVolumeSlider.value > 0 && muteMusicImage.sprite == MuteMusic_Button)
        {
            // change music volume
            music.volume = MusicVolumeSlider.value;
        }
        // if now muted (volume == 0)
        else
        {
            // switch the sprite, highlighted, pressed, and selected
            UnMuteMusicSpriteChange(muteMusicImage);

            // change music volume
            music.volume = MusicVolumeSlider.value;
        }

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);
    }

    /// <summary>
    /// Changes mute music button sprites to MuteBusic_Buton
    /// </summary>
    public void MuteMusicSpriteChange(Image muteMusicImage)
    {
        // switch the sprite, highlighted, pressed, and selected
        muteMusicImage.sprite = MuteMusic_Button;
        SpriteState muteMusicSpriteState = new SpriteState();
        muteMusicSpriteState.highlightedSprite = MuteMusic_Button_Highlighted;
        muteMusicSpriteState.pressedSprite = MuteMusic_Button_Highlighted;
        muteMusicSpriteState.selectedSprite = MuteMusic_Button_Highlighted;
        MuteMusicButton.spriteState = muteMusicSpriteState;
    }

    /// <summary>
    /// Changes mute music button sprites to UnMuteMusic_Buttton
    /// </summary>
    public void UnMuteMusicSpriteChange(Image muteMusicImage)
    {
        // switch the sprite, highlighted, pressed, and selected
        muteMusicImage.sprite = UnMuteMusic_Button;
        SpriteState muteMusicSpriteState = new SpriteState();
        muteMusicSpriteState.highlightedSprite = UnMuteMusic_Button_Highlighted;
        muteMusicSpriteState.pressedSprite = UnMuteMusic_Button_Highlighted;
        muteMusicSpriteState.selectedSprite = UnMuteMusic_Button_Highlighted;
        MuteMusicButton.spriteState = muteMusicSpriteState;
    }

    /// <summary>
    /// Handle mute effects button
    /// </summary>
    public void HandleMuteEffectsButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // get reference image
        Image muteEffectsImage = GameObject.FindWithTag("MuteEffects").GetComponent<Image>();

        // check which button is displayed and switch
        if (muteEffectsImage.sprite == MuteEffects_Button)
        {
            // switch the sprite, highlighted, pressed, and selected
            UnMuteEffectsSpriteChange(muteEffectsImage);

            // change music volume
            effects.volume = 0;

            // set music slider value
            EffectsVolumeSlider.value = effects.volume;
        }
        else
        {
            // switch the sprite, highlighted, pressed, and selected
            MuteEffectsSpriteChange(muteEffectsImage);

            // change music volume
            effects.volume = 0.5f;

            // set music slider value
            EffectsVolumeSlider.value = effects.volume;
        }

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);
    }

    /// <summary>
    /// Handles effects slider value change
    /// </summary>
    public void HandleEffectsSliderOnValueChangedEvent()
    {
        // get reference image
        Image muteEffectsImage = GameObject.FindWithTag("MuteEffects").GetComponent<Image>();

        // check which sprite is active on volume slider change
        // if was muted and now not muted
        if (EffectsVolumeSlider.value > 0 && muteEffectsImage.sprite == UnMuteEffects_Button)
        {
            // switch the sprite, highlighted, pressed, and selected
            MuteEffectsSpriteChange(muteEffectsImage);

            // change music volume
            effects.volume = EffectsVolumeSlider.value;
        }
        // if not muted and volume changed
        else if (EffectsVolumeSlider.value > 0 && muteEffectsImage.sprite == MuteEffects_Button)
        {
            // change music volume
            effects.volume = EffectsVolumeSlider.value;
        }
        // if now muted (volume == 0)
        else
        {
            // switch the sprite, highlighted, pressed, and selected
            UnMuteEffectsSpriteChange(muteEffectsImage);

            // change music volume
            effects.volume = EffectsVolumeSlider.value;
        }

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);
    }

    /// <summary>
    /// Changes mute music button sprites to MuteBusic_Buton
    /// </summary>
    public void MuteEffectsSpriteChange(Image muteEffectsImage)
    {
        // switch the sprite, highlighted, pressed, and selected
        muteEffectsImage.sprite = MuteEffects_Button;
        SpriteState muteEffectsSpriteState = new SpriteState();
        muteEffectsSpriteState.highlightedSprite = MuteEffects_Button_Highlighted;
        muteEffectsSpriteState.pressedSprite = MuteEffects_Button_Highlighted;
        muteEffectsSpriteState.selectedSprite = MuteEffects_Button_Highlighted;
        MuteEffectsButton.spriteState = muteEffectsSpriteState;
    }
              
    /// <summary>
    /// Changes mute music button sprites to UnMuteMusic_Buttton
    /// </summary>
    public void UnMuteEffectsSpriteChange(Image muteEffectsImage)
    {
        // switch the sprite, highlighted, pressed, and selected
        muteEffectsImage.sprite = UnMuteEffects_Button;
        SpriteState muteEffectsSpriteState = new SpriteState();
        muteEffectsSpriteState.highlightedSprite = UnMuteEffects_Button_Highlighted;
        muteEffectsSpriteState.pressedSprite = UnMuteEffects_Button_Highlighted;
        muteEffectsSpriteState.selectedSprite = UnMuteEffects_Button_Highlighted;
        MuteEffectsButton.spriteState = muteEffectsSpriteState;
    }
    #endregion
}
