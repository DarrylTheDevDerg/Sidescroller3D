using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    private int currentLevel;

    private void Start()
    {
        // Load the current level from PlayerPrefs when the game starts
        LoadCurrentLevel();
    }

    public void SetCurrentLevel(int level)
    {
        // Set the current level in PlayerPrefs
        PlayerPrefs.SetInt("CurrentLevel", level);

        // Save the changes to PlayerPrefs
        PlayerPrefs.Save();

        // Update the local current level variable
        currentLevel = level;
    }

    public void LoadCurrentLevel()
    {
        // Load the current level from PlayerPrefs
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1); // Default level is 1 if not set
    }

    public int GetCurrentLevel()
    {
        // Retrieve the current level
        return currentLevel;
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    public void LoadLevelScene()
    {
        if (currentLevel == 1)
        {
            SceneManager.LoadScene("LVL1");
        }

        if (currentLevel == 2)
        {
            SceneManager.LoadScene("LVL2");
        }

        if (currentLevel == 3)
        {
            SceneManager.LoadScene("LVL3");
        }
    }
}
