using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelTwoPauseMenu : MonoBehaviour
{
    public GameObject TwoPauseMenu;
    public AudioMixer LevelTwoMixer;
    public static bool isPaused;
    public void SetVolume(float volume)
    {
        LevelTwoMixer.SetFloat("LevelTwoVolume", volume);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Pause and Resume
    public void Start()
    {
        TwoPauseMenu.SetActive(false);
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
        TwoPauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        TwoPauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}
