using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Retry : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

}
