using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private List<string> scenesToLoad;
    private List<AsyncOperation> scenesToLoadA = new List<AsyncOperation>();
    public void LoadStartScenes()
    {
        foreach (string sceneName in scenesToLoad)
        {
            if (sceneName == scenesToLoad[0])
            {
                scenesToLoadA.Add(SceneManager.LoadSceneAsync(sceneName));
            }
            else
            {
                scenesToLoadA.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));
            }
        }  
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }
    //void OnSceneLoaded(Scene name, LoadSceneMode mode) => SceneManager.UnloadSceneAsync(0);
    //private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;
}
