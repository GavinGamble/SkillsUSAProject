using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomSong());
    }

    private IEnumerator PlayRandomSong()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, songs.Length);
            AudioClip selectedSong = songs[randomIndex];

            // Play the selected song
            audioSource.clip = selectedSong;
            audioSource.Play();

            // Wait until the song is finished playing
            yield return new WaitForSeconds(selectedSong.length);

            // Remove the played song from the array
            songs = RemoveAt(songs, randomIndex);
        }
    }

    private T[] RemoveAt<T>(T[] array, int index)
    {
        T[] newArray = new T[array.Length - 1];
        for (int i = 0, j = 0; i < array.Length; i++)
        {
            if (i != index)
            {
                newArray[j++] = array[i];
            }
        }
        return newArray;
    }
}
