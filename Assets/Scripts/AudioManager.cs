using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip level2;
    public AudioClip win;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Object.FindObjectsByType<AudioManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Ranking Menu":
                PlayMusic(menuMusic);
                break;
            
            case "MainMenu":
                PlayMusic(menuMusic);
                break;

            case "SampleScene":
                PlayMusic(level1Music);
                break;

            case "Level 2":
                PlayMusic(level2);
                break;
            case "Win":
                PlayMusic(win);
                break;
            default:
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; 
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
