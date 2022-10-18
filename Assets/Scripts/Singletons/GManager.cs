using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

public class GManager : MonoBehaviour
{
    public static GManager instance;
    public string loseScreen;
    public event Action onGameStartEvent;
    public event Action GameRestart;
    public event Action GameLost;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        onGameStartEvent?.Invoke();
        GameLost += changeSceneToLoseScreen;
    }
    public void CallGameLostEvent() => GameLost?.Invoke();

    public void changeSceneToLoseScreen()
    {
        SceneManager.LoadScene(loseScreen);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
