using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Retry : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
