using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Button support
/// </summary>
public class Buttons : MonoBehaviour
{
    #region Fields

    [SerializeField]
    Canvas prefabPauseMenu;

    #endregion

    #region Unity Methods

    private void Start()
    {
        // restart button only active if player has money
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            // set restart button
            if (PlayerPrefs.GetInt(PlayerPrefNames.LifetimeAliensDefeated.ToString()) > 0)
            {
                Button button = GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>();
                button.enabled = true;
                button.interactable = true;
            }
            else
            {
                Button button = GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>();
                button.enabled = false;
                button.interactable = false;
            }

            // set alien store button
            if (PlayerPrefs.GetInt(PlayerPrefNames.AlienStoreUnlocked.ToString()) == 1)
            {
                Button button = GameObject.FindGameObjectWithTag("AlienStore").GetComponent<Button>();
                button.enabled = true;
                button.interactable = true;
            }
            else
            {
                Button button = GameObject.FindGameObjectWithTag("AlienStore").GetComponent<Button>();
                button.enabled = false;
                button.interactable = false;
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Handles play button click
    /// </summary>
    public void HandlePlayButtonOnClickEvent()
    {
        // Debugging ---- REMOVE
        //PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), 90000000000000);
        //PlayerPrefs.SetInt(PlayerPrefNames.AlienStoreUnlocked.ToString(), 1);
        //PlayerPrefs.SetInt(PlayerPrefNames.HasTargetingSystem.ToString(), 1);
        //PlayerPrefs.SetInt(PlayerPrefNames.HasMoneyMultiplier.ToString(), 1);
        //PlayerPrefs.SetInt(PlayerPrefNames.HasMagnet.ToString(), 1);



        // play select
        AudioManager.Play(AudioName.Select);

        // play background music
        if (LoopingAudioManager.Playing == AudioName.Ambient)
        {
            LoopingAudioManager.Switch(AudioName.GamePlayAmbient);
        }
        
        // reset gameplay values
        PlayerPrefs.SetInt(PlayerPrefNames.RoundsCompleted.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.AliensDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.RedsDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.GreensDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.MotherShipsDefeated.ToString(), 0);

        // set time scale
        Time.timeScale = 1.0f;

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("GamePlay");

        // load game play
        //SceneManager.LoadScene("GamePlay");
    }

    /// <summary>
    /// Handles restarting the game
    /// </summary>
    public void HandleRestartButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // reset tracking player prefs
        PlayerPrefs.SetInt(PlayerPrefNames.LifetimeAliensDefeated.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.AlienStoreUnlocked.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0);

        // reset store player prefs
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserSpeed.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserDamage.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLaserCooldown.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipMoveSpeed.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.ShipLifeAmount.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.HealthCost.ToString(), ConfigUtils.LifeAmountCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.MoveCost.ToString(), ConfigUtils.MoveSpeedCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserDamageCost.ToString(), ConfigUtils.LaserDamageCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserSpeedCost.ToString(), ConfigUtils.LaserSpeedCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.LaserCooldownCost.ToString(), ConfigUtils.LaserCooldownCost);

        // reset alien store player prefs
        PlayerPrefs.SetFloat(PlayerPrefNames.TargetingChance.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalChance.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalDamage.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.MoneyMultiplier.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.MagnetRange.ToString(), 0);
        PlayerPrefs.SetFloat(PlayerPrefNames.TargetingChanceCost.ToString(), ConfigUtils.TargetingChanceCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalChanceCost.ToString(), ConfigUtils.CritChanceCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.CriticalDamageCost.ToString(), ConfigUtils.CritDamageCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.MoneyMultiplierCost.ToString(), ConfigUtils.MoneyMultiplierCost);
        PlayerPrefs.SetFloat(PlayerPrefNames.MagnetRangeCost.ToString(), ConfigUtils.MagnetRangeCost);
        PlayerPrefs.SetInt(PlayerPrefNames.HasTargetingSystem.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.HasMoneyMultiplier.ToString(), 0);
        PlayerPrefs.SetInt(PlayerPrefNames.HasMagnet.ToString(), 0);

        // set restart inactive
        Button button = GameObject.FindGameObjectWithTag("Restart").GetComponent<Button>();
        button.enabled = false;
        button.interactable = false;

        // set alien store button
        Button buttonA = GameObject.FindGameObjectWithTag("AlienStore").GetComponent<Button>();
        buttonA.enabled = false;
        buttonA.interactable = false;
    }

    /// <summary>
    /// Handles setting button click
    /// </summary>
    public void HandleSettingsButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("Settings");
    }

    /// <summary>
    /// Handles quit button click
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

    /// <summary>
    ///  Handles pause button click
    /// </summary>
    public void HandlePauseButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // set pause button
        Button buttonA = GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>();
        buttonA.enabled = false;
        buttonA.interactable = false;

        // deselct the button
        EventSystem.current.SetSelectedGameObject(null);

        // set time scale
        Time.timeScale = 0;

        // display pause menu
        Instantiate(prefabPauseMenu);
    }

    /// <summary>
    /// Handles pause menu quit button click
    /// </summary>
    public void HandlePauseQuitButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // play ambient music
        LoopingAudioManager.Switch(AudioName.Ambient);

        // set time scale
        Time.timeScale = 1.0f;

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("MainMenu");
    }

    /// <summary>
    /// Handles pause menu resume button click
    /// </summary>
    public void HandlePauseResumeButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // set pause button
        Button buttonA = GameObject.FindGameObjectWithTag("Pause").GetComponent<Button>();
        buttonA.enabled = true;
        buttonA.interactable = true;

        // set time scale
        Time.timeScale = 1.0f;

        // destroy pause menu
        Destroy(GameObject.FindGameObjectWithTag("PauseMenu"));
    }

    /// <summary>
    /// Handles store button click
    /// </summary>
    public void HandleStoreButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("Store");
    }

    /// <summary>
    /// Handles alien store button click
    /// </summary>
    public void HandleAlienStoreButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("AlienStore");
    }

    /// <summary>
    /// Handles main menu button click
    /// </summary>
    public void HandleMainMenuButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // crossfade load
        GameObject.Find("LevelLoaderFade").GetComponent<LevelLoader>().LoadNextScene("MainMenu");
    }

    #endregion
}
