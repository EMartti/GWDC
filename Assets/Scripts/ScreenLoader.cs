using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    public enum Scene { StartMenu_2, VerticalSlice_2 };
    [SerializeField] private Scene sceneToLoad;

    public void ButtonLoadScene()
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}