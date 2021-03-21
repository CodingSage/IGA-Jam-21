using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject levelMenuPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && levelMenuPanel != null)
        {
            levelMenuPanel.SetActive(!levelMenuPanel.activeSelf);
        }
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < sceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
