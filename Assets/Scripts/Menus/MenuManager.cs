using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handle menu navigation
/// </summary>
public static class MenuManager
{
    public static void GoToMenu(MenuName name)
    {
        switch (name)
        {
            case MenuName.MainMenu:
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
