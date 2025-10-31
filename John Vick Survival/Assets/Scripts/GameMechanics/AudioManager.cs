using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip[] levelMusic; // Assign 5 songs in Inspector

    private void Awake()
    {
        // Singleton pattern to persist across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // Start with menu music
        PlayMenuMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.ToLower().Contains("menu"))
        {
            PlayMenuMusic();
        }
        else if (scene.name.ToLower().Contains("level"))
        {
            PlayRandomLevelMusic();
        }
    }

    public void PlayMenuMusic()
    {
        if (musicSource.clip == menuMusic && musicSource.isPlaying)
            return;

        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayRandomLevelMusic()
    {
        if (levelMusic.Length == 0)
            return;

        // Pick random song
        AudioClip randomClip = levelMusic[Random.Range(0, levelMusic.Length)];

        // Avoid restarting if already playing that clip
        if (musicSource.clip == randomClip && musicSource.isPlaying)
            return;

        musicSource.clip = randomClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
