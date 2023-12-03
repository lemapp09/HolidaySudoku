using UnityEngine;

public class MusicPlayer : MonoSingleton<MusicPlayer>
{
    public AudioClip[] playlist1;
    public AudioClip[] playlist2;
    private AudioClip[] currentPlaylist;

    private AudioSource audioSource;
    private int currentTrackIndex = -1; // Start before the first index

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Set the default playlist
        currentPlaylist = playlist1;
        PlayNextSong();
    }

    void Update()
    {
        // Check if the song has finished playing
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    // Call this method to switch between playlists
    public void SwitchPlaylist()
    {
        if (currentPlaylist == playlist1)
        {
            currentPlaylist = playlist2;
        }
        else
        {
            currentPlaylist = playlist1;
        }
        // Reset the current track index and play the next song
        currentTrackIndex = -1;
        PlayNextSong();
    }

    // Plays the next song in the current playlist
    private void PlayNextSong()
    {
        currentTrackIndex++;
        // If we reached the end of the playlist, loop back to the start
        if (currentTrackIndex >= currentPlaylist.Length)
        {
            currentTrackIndex = 0;
        }
        // Assign the next song and play it
        audioSource.clip = currentPlaylist[currentTrackIndex];
        audioSource.Play();
    }
}