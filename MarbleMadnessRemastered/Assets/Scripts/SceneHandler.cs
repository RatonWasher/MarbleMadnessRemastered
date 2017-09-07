using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    //values to know what scene we're in
    private bool isInCampain = false;
    private bool isInQuickplay = false;
    private bool isInEditor = false;
    public bool isOnline = false;
    public bool isTimed = false;
    private bool isPlaying = true;
    public string slotUsed;


    //all UI objects
    private GameObject campain;
    private GameObject quickplay;
    private GameObject editor;


    private GameObject cam;

    //in-Game UI
    private GameObject canvas;
    private GameObject backButton;
    private GameObject resume;
    private GameObject reload;
    private GameObject quit;
    private GameObject pausePanel;
    private GameObject healthField;
    private GameObject healthText;
    private GameObject timerField;
    private GameObject timerText;
    private GameObject GameOverImage;



    float[] durationValues = new float[8] {40, 75, 170, 105, 140, 210, 240, 420};
    GameObject[] pauseObjects;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);//the object will be persistent through the next scenes
	}

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 7 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            if (!isOnline && !isInEditor)
            {
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                {
                    if (Time.timeScale == 1)
                    {
                        Time.timeScale = 0;
                        showPaused();
                    }
                    else if (Time.timeScale == 0)
                    {
                        Time.timeScale = 1;
                        hidePaused();
                    }
                }
            }
            
        }
    }


    void OnLevelWasLoaded(int level)//the int is a build value representing the order of the scenes
    {
        if (level == 2)//solo menu code
        {
            isOnline = false;
            campain = GameObject.Find("CampainButton");
            Button campainButton = campain.GetComponent<Button>();
            campainButton.onClick.AddListener(delegate { setCampain(); });

            quickplay = GameObject.Find("QuickplayButton");
            Button quickplayButton = quickplay.GetComponent<Button>();
            quickplayButton.onClick.AddListener(delegate { setQuickplay(true); });

            editor = GameObject.Find("EditorButton");
            Button editorButton = editor.GetComponent<Button>();
            editorButton.onClick.AddListener(delegate { setEditor(); });
        }

        if(level == 5) //editor scene
        {
            if (isInQuickplay)
            {
                GameObject editorModePanel = GameObject.Find("Canvas/EditorMode");
                GameObject quickplayModePanel = GameObject.Find("Canvas/QuickplayMode");

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                resume = GameObject.Find("Canvas/QuickplayMode/PausePanel/ResumeButton");
                Button resumeButton = resume.GetComponent<Button>();
                resumeButton.onClick.AddListener(delegate { pauseControl(); });
                reload = GameObject.Find("Canvas/QuickplayMode/PausePanel/ReloadButton");
                Button reloadButton = reload.GetComponent<Button>();
                reloadButton.onClick.AddListener(delegate { Reload(); });
                quit = GameObject.Find("Canvas/QuickplayMode/PausePanel/QuitButton");
                Button quitButton = quit.GetComponent<Button>();
                quitButton.onClick.AddListener(delegate { quitGame(); });

                healthText = GameObject.Find("Canvas/QuickplayMode/HealthImage/Text");
                timerField = GameObject.Find("Canvas/QuickplayMode/TimeImage");
                timerText = GameObject.Find("Canvas/QuickplayMode/TimeImage/Text");
                GameOverImage = GameObject.Find("Canvas/QuickplayMode/GameOverImage");


                editorModePanel.SetActive(false);
                timerField.SetActive(false);
                GameOverImage.SetActive(false);

                Time.timeScale = 1;
                pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
                hidePaused();
            }
            else if (isInEditor)
            {
                GameObject editorModePanel = GameObject.Find("Canvas/EditorMode");
                GameObject quickplayModePanel = GameObject.Find("Canvas/QuickplayMode");

                GameObject quitAndSave = GameObject.Find("Canvas/EditorMode/Menu/SaveAndQuitButton");
                quitAndSave.GetComponent<Button>().onClick.AddListener(delegate { quitGame(); });
                quit = GameObject.Find("Canvas/EditorMode/Menu/QuitButton");
                Button quitButton = quit.GetComponent<Button>();
                quitButton.onClick.AddListener(delegate { quitGame(); });
                quickplayModePanel.SetActive(false);
            }
            
        }

        if (level == 6)//online lobby scene
        {
            isOnline = true;
            backButton = GameObject.Find("LobbyManager/MainPanel/BackToMenuButton");
        }

        if (level >= 7 && level < 15)//all playable levels
        {
            if (!isOnline && !isInEditor){

                //instantiate the player
                GameObject player = Instantiate(Resources.Load("Player")) as GameObject;
                Transform playerSpawn = GameObject.Find("Spawns/Respawn").GetComponent<Transform>();
                player.transform.position = playerSpawn.position;
                

                //setup for the in game UI on start
                resume = GameObject.Find("Canvas/PausePanel/ResumeButton");
                Button resumeButton = resume.GetComponent<Button>();
                resumeButton.onClick.AddListener(delegate { pauseControl(); });
                reload = GameObject.Find("Canvas/PausePanel/ReloadButton");
                Button reloadButton = reload.GetComponent<Button>();
                reloadButton.onClick.AddListener(delegate { Reload(); });
                quit = GameObject.Find("Canvas/PausePanel/QuitButton");
                Button quitButton = quit.GetComponent<Button>();
                quitButton.onClick.AddListener(delegate { quitGame(); });

                healthText = GameObject.Find("Canvas/HealthImage/Text");
                timerField = GameObject.Find("Canvas/TimeImage");
                timerText = GameObject.Find("Canvas/TimeImage/Text");
                GameOverImage = GameObject.Find("Canvas/GameOverImage");

                //activate the timer only if selected
                if (!isTimed || !isInCampain)
                {
                    timerField.SetActive(false);                    
                    player.GetComponent<TimerManager>().enabled = false;
                }
                else
                {
                    player.GetComponent<TimerManager>().Duration = durationValues[level-7];
                }

                GameOverImage.SetActive(false);

                Time.timeScale = 1;
                pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
                hidePaused();

                
            }
            else if(isOnline)
            {
                //set the editor mode UI
                pauseObjects = GameObject.FindGameObjectsWithTag("Pause");
                hidePaused();

                timerField = GameObject.Find("Canvas/TimeImage");
                GameOverImage = GameObject.Find("Canvas/GameOverImage");
                healthText = GameObject.Find("Canvas/HealthImage/Text");

                timerField.SetActive(false);
                GameOverImage.SetActive(false);
            }
            
        }

        if (level == 15)//win scene
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("MainMenu");
            }
        }

        //Music button
        if (level == 1 || level == 2)
        {
            GameObject.Find("Canvas/OptionsButton/MusicButton").GetComponent<Button>().onClick.
                AddListener(delegate { ToggleMusic(); }); ;
        }   
    }

    public void setCampain()
    {
        isInCampain = true;
    }
	
    public void setQuickplay(bool value)
    {
        isInQuickplay = value;
    }

    public void setEditor()
    {
        isInEditor = true;
    }

    public bool getIsCampain()
    {
        return isInCampain;
    }

    public bool getIsEditor()
    {
        return isInEditor;
    }

    public bool getIsQuickplay()
    {
        return isInQuickplay;
    }

    public void activateCamera()
    {
        cam = GameObject.Find("Main Camera");
        if (!isOnline)
        {
            cam.GetComponent<CameraFollow>().enabled = true;
        }
    }

    //Reloads the Level
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with Pause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with Pause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void quitGame()
    {
        if (isInQuickplay)
        {
            isInQuickplay = false;
            SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("LevelSelect");
        }
        else if(isInCampain)
        {
            isInCampain = false;
            Time.timeScale = 1;
            SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("SoloMenu");
        }
        else if (isInEditor)
        {
            isInEditor = false;
            Time.timeScale = 1;
            SceneManager.LoadScene("SoloMenu");
        }
        
    }

    public void setHealthText(string text)
    {
        healthText.GetComponent<Text>().text = text;
    }

    public void setTimerText(string text)
    {
        timerText.GetComponent<Text>().text = text;
    }

    public void setGameOver(bool hasLost)
    {
        GameOverImage.SetActive(true);
        if (hasLost)
        {
            GameOverImage.GetComponentInChildren<Text>().text = "You lose";
        }
        else
        {
            GameOverImage.GetComponentInChildren<Text>().text = "You win";
        }
        
    }

    public void showGameOver()
    {
        GameOverImage.SetActive(true);
    }

    public void setSlot(string value)
    {
        slotUsed = value;
    }

    public int getActiveScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void ToggleMusic()
    {
        if (isPlaying)
            GetComponent<AudioSource>().Pause();
        else
            GetComponent<AudioSource>().Play();
        isPlaying = !isPlaying;
    }

}
