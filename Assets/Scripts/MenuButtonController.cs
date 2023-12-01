using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    // Brandon its jeff.. would like to see use of private. and camelCase variable names, keeping consistent
    // like mIndex, mKeyDown, mMaxIndex, mAudioSource.
    // also no need to expose all variables to the inspector if you dont need to adjust them in the inspector
    // just my thoughts
    // also was thinking maybe some different text font would be nice.

    [SerializeField] private int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis ("Vertical") !=0)
        {
            if(!keyDown)
            {
                if(Input.GetAxis ("Vertical") < 0)
                {
                    if(index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                   
                }
                else if(Input.GetAxis ("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
