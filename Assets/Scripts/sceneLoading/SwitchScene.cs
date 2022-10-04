using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string currentScene;
    private bool activated = false;
    private void SceneSwitcher()
    {
        activated = true;
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        PlayerManager.instance.playerUtils.LevelPositionChange(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!activated)
            SceneSwitcher();
    }
}
