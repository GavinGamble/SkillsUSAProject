using UnityEngine;
using TMPro;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource audioSource;
    private int currentSongIndex = -1; // Start with -1 to indicate no song is playing

    public TMP_Text songText; // Reference to the TextMeshPro Text component

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomSong();
    }

    void UpdateSongText()
    {
        if (songText != null && currentSongIndex >= 0 && currentSongIndex < songs.Length)
        {
            songText.text = "Now Playing: " + songs[currentSongIndex].name;
        }
    }

    void PlayRandomSong()
    {
        if (songs.Length > 0)
        {
            currentSongIndex = Random.Range(0, songs.Length);
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();
            UpdateSongText(); // Update the song text when playing a new song
        }
    }

    public void PlayNextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % songs.Length;
        PlayCurrentSong();
    }

    public void PlayPreviousSong()
    {
        currentSongIndex = (currentSongIndex - 1 + songs.Length) % songs.Length;
        PlayCurrentSong();
    }

    void PlayCurrentSong()
    {
        if (currentSongIndex >= 0 && currentSongIndex < songs.Length)
        {
            audioSource.clip = songs[currentSongIndex];
            audioSource.Play();
            UpdateSongText(); // Update the song text when playing a new song
        }
    }
}


