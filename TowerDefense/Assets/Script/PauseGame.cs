using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public bool GameisPaused;
    [SerializeField]
    GameObject PauseUI;

    void Start()
    {
        GameisPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;    
        GameisPaused = false;
        PauseUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameisPaused = true;
        PauseUI.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
