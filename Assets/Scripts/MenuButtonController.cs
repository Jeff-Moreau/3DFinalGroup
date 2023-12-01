using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
   
    // also was thinking maybe some different text font would be nice.

    [SerializeField] private int mIndex;
    [SerializeField] bool mKeyDown;
    [SerializeField] int mMaxIndex;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis ("Vertical") !=0)
        {
            if(!mKeyDown)
            {
                if(Input.GetAxis ("Vertical") < 0)
                {
                    if(mIndex < mMaxIndex)
                    {
                        mIndex++;
                    }
                    else
                    {
                        mIndex = 0;
                    }
                   
                }
                else if(Input.GetAxis ("Vertical") > 0)
                {
                    if (mIndex > 0)
                    {
                        mIndex--;
                    }
                    else
                    {
                        mIndex = mMaxIndex;
                    }
                }
                mKeyDown = true;
            }
        }
        else
        {
            mKeyDown = false;
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
