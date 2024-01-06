using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject PauseMenuUI;

    private void OnEnable()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    private void OnDisable()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    private void OnDestroy()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void ResumeButton()
    {
        GetComponentInParent<InventoryVisibilityScript>().hide();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        foreach(DropItemChance i in FindObjectsByType<DropItemChance>(FindObjectsSortMode.None))
        {
            i.setQuitTrue();
        }
        SceneManager.LoadScene("main_menu");
    }
}
