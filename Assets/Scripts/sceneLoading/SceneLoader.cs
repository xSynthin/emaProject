using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private List<string> scenesToLoad;
    void Start()
    {
        foreach (string sceneName in scenesToLoad) SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene name, LoadSceneMode mode) => SceneManager.UnloadSceneAsync(0);
    private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;
}
