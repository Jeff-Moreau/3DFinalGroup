using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // looks like some of your menu options are a little off
    // options are overlapping on my end, Save and Quit and Resume Game is line wrapping oddly.
    // again just some thoughts.
    // also was thinking maybe some different text font would be nice.

    [SerializeField] private GameObject PausePanel;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
