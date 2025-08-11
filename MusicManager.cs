using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    [Range(0f, 1f)] public float normalVolume = 0.05f;
    public string[] mutedScenes;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = normalVolume;
        audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool mute = false;
        foreach (string name in mutedScenes)
        {
            if (scene.name == name)
            {
                mute = true;
                break;
            }
        }

        audioSource.volume = mute ? 0f : normalVolume;
    }
}
