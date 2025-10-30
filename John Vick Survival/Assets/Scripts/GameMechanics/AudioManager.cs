using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; 

    [Header("Music Clips")]
    public AudioClip normalMusic;
    public AudioClip bossMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayNormalMusic();
    }

    public void PlayNormalMusic()
    {
        if (musicSource.clip == normalMusic) return;
        musicSource.clip = normalMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayBossMusic()
    {
        if (musicSource.clip == bossMusic) return;
        musicSource.clip = bossMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
