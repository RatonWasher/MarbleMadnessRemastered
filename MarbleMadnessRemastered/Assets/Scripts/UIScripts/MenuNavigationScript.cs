using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuNavigationScript : MonoBehaviour
{

    public Button soundButton;
    public Button musicButton;    
    public Animator musicAnimator;
    public Animator soundAnimator;
    private bool optionsClicked = false;


    // Update is called once per frame
    public void optionButtonClicked()
    {
        optionsClicked = !optionsClicked;
        if (optionsClicked)
        {
            musicAnimator.Play("MoveInMusic");
            soundAnimator.Play("MoveInSound");
        }
        else
        {
            musicAnimator.Play("MoveOutMusic");
            soundAnimator.Play("MoveOutSound");
        }
    }

    public void loadCampain()
    {
        SceneManager.LoadScene("SoloMenu");
    }

    public void loadOnlineLobby()
    {
        SceneManager.LoadScene("OnlineLobby");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}