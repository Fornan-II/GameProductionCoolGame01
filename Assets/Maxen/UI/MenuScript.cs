using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    protected int activeMenuIndex = -1;
    protected int previousMenuIndex = -1;
    public SubMenu[] MenuScreens;
    public int StartingMenu = 0;

    public bool AllowPausing = false;

    [Header("SceneIndices")]
    public int GameSceneIndex = 1;
    public int MenuSceneIndex = 0;
    
    public bool IsPaused
    {
        get;
        protected set;
    }

    private void Start()
    {
        foreach (SubMenu menu in MenuScreens)
        {
            if (menu)
            {
                menu.SetActive(false);
            }
        }
        ChangeMenuTo(StartingMenu);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel") && AllowPausing)
        {
            TogglePause();
        }
    }

    //MAIN MENU FUNCTIONALITY
    //
    //

    public void PlayGame()
    {
        SceneManager.LoadScene(GameSceneIndex);
    }

    //Navigate between menus.
    public void ChangeMenuTo(int newMenuIndex)
    {
        //Debug.Log("Previous:" + previousMenuIndex + ", Active:" + activeMenuIndex + ", New:" + newMenuIndex);
        if (newMenuIndex != activeMenuIndex)
        {
            if (0 <= activeMenuIndex && activeMenuIndex < MenuScreens.Length)
            {
                if (MenuScreens[activeMenuIndex])
                {
                    MenuScreens[activeMenuIndex].SetActive(false);
                }
            }
            if (0 <= newMenuIndex && newMenuIndex < MenuScreens.Length)
            {
                if (MenuScreens[newMenuIndex])
                {
                    MenuScreens[newMenuIndex].SetActive(true);
                }
            }

            previousMenuIndex = activeMenuIndex;
            activeMenuIndex = newMenuIndex;
        }
    }

    //Navigate to previous menu
    public void BackToPreviousMenu()
    {
        ChangeMenuTo(previousMenuIndex);
    }

    //Also used in pause menu
    public void QuitGame()
    {
        Application.Quit();
    }

    //
    //
    //PAUSE MENU FUNCTIONALITY
    //
    //
    public void ResumeGame()
    {
        ChangeMenuTo(0);
        TimeManager.ResumeTime();
        IsPaused = false;
    }

    public void TogglePause()
    {
        if (AllowPausing)
        {
            if (!IsPaused)
            {
                ChangeMenuTo(1);
                TimeManager.StopTime();
                IsPaused = true;
            }
            else
            {
                ChangeMenuTo(0);
                TimeManager.ResumeTime();
                IsPaused = false;
            }
        }
    }

    public void ReturnToMainMenu()
    {
        TimeManager.ResumeTime();
        SceneManager.LoadScene(MenuSceneIndex);
    }
}
