using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float updateInterval = 1f;

    private float elapsedTime = 0f;

    void Start()
    {
        // Start the timer
        InvokeRepeating("UpdateTimer", 0f, updateInterval);
    }

    void UpdateTimer()
    {
        // Update elapsed time
        elapsedTime += updateInterval;

        // Format and display the timer text
        timerText.text = FormatTime(elapsedTime);
    }

    string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        if (hours > 0)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else if (minutes > 0)
        {
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            return string.Format("{0}", seconds);
        }
    }
}


