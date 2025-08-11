using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button skipButton;
    public string nextLevel;

    void Start()
    {
        if (videoPlayer == null)
            videoPlayer = FindObjectOfType<VideoPlayer>();

        if (skipButton == null)
            skipButton = FindObjectOfType<Button>();

        if (videoPlayer == null || skipButton == null)
        {
            Debug.LogError("VideoPlayer sau SkipButton nu au fost găsite!");
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
        skipButton.onClick.AddListener(SkipVideo);

        if (string.IsNullOrEmpty(nextLevel))
        {
            nextLevel = "Level1"; // fallback
        }

        // DEBUG: verifică valoarea
        Debug.Log("Următorul nivel este: " + nextLevel);
    }


    void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextLevel();
    }

    void SkipVideo()
    {
        videoPlayer.Stop();
        LoadNextLevel();
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}
