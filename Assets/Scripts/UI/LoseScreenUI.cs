using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreenUI : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
