using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Current;
    private Scene lastLoadedScene;
    void Start()
    {
        Current = this;
        if (PlayerPrefs.GetInt("currentLevel") == 30)
            ChangeLevel("Last");
        else
            ChangeLevel("Level " + PlayerPrefs.GetInt("currentLevel"));
    }
    public void ChangeLevel(string sceneName)
    {
        StartCoroutine(ChangeScene(sceneName));
    }
    
    IEnumerator ChangeScene(string sceneName)
    {
        if(lastLoadedScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(lastLoadedScene);
            bool sceneUnloaded = false;
            while(!sceneUnloaded)
            {
                sceneUnloaded = !lastLoadedScene.IsValid();
                yield return new WaitForEndOfFrame();
            }
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        bool sceneLoaded = false;
        while(!sceneLoaded)
        {
            lastLoadedScene = SceneManager.GetSceneByName(sceneName);
            sceneLoaded = lastLoadedScene != null && lastLoadedScene.isLoaded;
            yield return new WaitForEndOfFrame();
        }
      
    }
}
