using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    private AudioSource audio;

    [Header("Menu Music")]
    public AudioClip[] menuMusics;

    [Header("Level Music")]
    public AudioClip[] levelMusics;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audio = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // MENU
        if (scene.name == "MainMenu" || scene.name == "LevelSelect")
        {
            PlayRandom(menuMusics);
        }
        // LEVEL
        else if (scene.name.StartsWith("Level"))
        {
            PlayRandom(levelMusics);
        }
    }

    void PlayRandom(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        AudioClip randomClip = clips[Random.Range(0, clips.Length)];

        if (audio.clip == randomClip && audio.isPlaying) return;

        audio.clip = randomClip;
        audio.Play();
    }

    public void StopBGM()
    {
        audio.Stop();
    }
}
