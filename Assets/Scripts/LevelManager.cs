using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    private const int PRELOADER_INDEX = 0;

    private static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static LevelManager getInstance()
    {
        if (instance == null)
            instance = new LevelManager();
        return instance;
    }

    public void reloadlevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    public void loadPreloader()
    {
        SceneManager.LoadScene(PRELOADER_INDEX);
    }

}
