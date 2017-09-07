using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoloMenuUI : MonoBehaviour {

    public Button soundButton;
    public Button musicButton;
    public Button checkbox;
    public Animator musicAnimator;
    public Animator soundAnimator;
    public Sprite cross;
    public Sprite check;
    private SceneHandler sceneManager;

    private bool optionsClicked = false;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
    }
    public void campainButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void quickPlayButtonClicked()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void backToSoloMenu()
    {
        sceneManager.setQuickplay(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("SoloMenu");
    }

    public void backToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void loadCampain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void loadOnlineLobby()
    {
        SceneManager.LoadScene("OnlineLobby");
    }

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

    public void loadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void checkBoxClicked()
    {
        sceneManager.isTimed = !sceneManager.isTimed;
        if (sceneManager.isTimed)
        {
            checkbox.GetComponent<Image>().sprite = check;
        }
        else
        {
            checkbox.GetComponent<Image>().sprite = cross;
        }
    }

    public void editorButtonClicked()
    {
        SceneManager.LoadScene("EditorMenu");
    }
}
