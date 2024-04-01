using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SandboxPauseMenu : MonoBehaviour
{
    public GameObject SBPauseMenu;
    public AudioMixer SandBoxMixer;
    public static bool isPaused;
    public void SetVolume(float volume)
    {
        SandBoxMixer.SetFloat("SandboxVolume", volume);
    }
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Pause and Resume
    public void Start()
    {
        SBPauseMenu.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
;
        }
    }

    public void PauseGame()
    {
        SBPauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        SBPauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}
