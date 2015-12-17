using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{
    public AudioSource audio;

    public AudioClip[] songs;

    public static MusicManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void playOnShot(int id)
    {
        audio.PlayOneShot(songs[id]);
    }
}
