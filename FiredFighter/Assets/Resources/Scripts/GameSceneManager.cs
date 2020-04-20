using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance;
    public string[] levelScene;
    public int level = -1;

    void Start()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
        level++;
        if(level >= levelScene.Length)
        {
            SceneManager.LoadScene("EndCredit");
        } else
        {
           SceneManager.LoadScene(levelScene[level]);
        }
    }

    public void LoadPreviousGameScene()
    {
        SceneManager.LoadScene(levelScene[level]);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
