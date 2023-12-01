using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private static bool GameisPaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
                Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
