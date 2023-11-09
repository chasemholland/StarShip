using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Button support
/// </summary>
public class Buttons : MonoBehaviour
{
    #region Fields

    [SerializeField]
    Canvas prefabPauseMenu;

    [SerializeField]
    Canvas prefabStoreMenu;

    #endregion

    #region Methods

    private void Start()
    {
        // restart button only active if player has money
        if (SceneManager.GetActiveScene().name == "MainMenu" && GameObject.FindGameObjectWithTag("StoreMenu") == null)
        {
            if (PlayerPrefs.GetFloat(PlayerPrefNames.PlayerMoney.ToString()) <= 0)
            {
                GameObject.FindGameObjectWithTag("RestartButton").SetActive(false);
            }
            else
            {
                GameObject.FindGameObjectWithTag("RestartButton").SetActive(true);
            }
        }
    }

    /// <summary>
    /// Handles play button click
    /// </summary>
    public void HandlePlayButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // play background music
        if (LoopingAudioManager.Playing == AudioName.Ambient)
        {
            LoopingAudioManager.Switch(AudioName.GamePlayAmbient);
        }

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GamePlay");
    }

    /// <summary>
    /// Handles restarting the game
    /// </summary>
    public void HandleRestartButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        // reset player prefs
        PlayerPrefs.SetFloat(PlayerPrefNames.PlayerMoney.ToString(), 0);
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

        // remove the restart button
        GameObject.FindGameObjectWithTag("RestartButton").SetActive(false);
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

        Time.timeScale = 0;
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

        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Handles pause menu resume button click
    /// </summary>
    public void HandlePauseResumeButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        Time.timeScale = 1.0f;
        Destroy(GameObject.FindGameObjectWithTag("PauseMenu"));
    }

    /// <summary>
    /// Handles pause menu store button click
    /// </summary>
    public void HandleStoreButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        Instantiate(prefabStoreMenu);
    }

    /// <summary>
    /// Handles main menu button click
    /// </summary>
    public void HandleMainMenuButtonOnClickEvent()
    {
        // play select
        AudioManager.Play(AudioName.Select);

        SceneManager.LoadScene("mainMenu");
    }

    #endregion
}
