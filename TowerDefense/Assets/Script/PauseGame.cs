using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool GameisPaused;

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

    void Resume()
    {
        Time.timeScale = 1f;    
        GameisPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameisPaused = true;
    }


}
