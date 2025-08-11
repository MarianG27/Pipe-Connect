using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public Button unlockAllButton;
    public Button resetProgressButton;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                buttons[i].interactable = true;
                buttons[i].image.color = Color.white;
            }
            else
            {
                buttons[i].interactable = false;
                buttons[i].image.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }

        unlockAllButton.onClick.AddListener(UnlockAllLevels);
        resetProgressButton.onClick.AddListener(ResetProgress);
    }

    public void OpenLevel(int levelId)
    {
        // Deblocăm următorul nivel doar dacă e mai mare decât ce e deja salvat
        int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (levelId + 1 > currentUnlocked && levelId < buttons.Length)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelId + 1);
            PlayerPrefs.Save();
        }

        string levelName = "Level" + levelId;
        string videoSceneName = "VideoScene_" + levelName;

        if (Application.CanStreamedLevelBeLoaded(videoSceneName))
        {
            PlayerPrefs.SetString("NextLevelToLoad", levelName);
            PlayerPrefs.Save();
            SceneManager.LoadScene(videoSceneName);
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
    }

    void UnlockAllLevels()
    {
        PlayerPrefs.SetInt("UnlockedLevel", buttons.Length);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetProgress()
    {
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
