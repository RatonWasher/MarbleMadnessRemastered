//Attach this script to the ending gO with a trigger collider.
//Ends the level when a gO triggers it.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    public bool tornadoEnd;
    private SceneHandler sceneManager;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (tornadoEnd)
        {
            collider.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collider.GetComponent<Controls>().loseControls();
        }
        else
        {
            collider.GetComponent<Controls>().Freeze();
        }

        if (!sceneManager.isOnline)
        {
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<Fade>().StartFade(1);
            if (sceneManager.getIsQuickplay())
            {
                SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("LevelSelect");
            }
            if (sceneManager.getIsCampain())
            {
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("Player").GetComponent<TimerManager>().stopTimer();
                StartCoroutine(LoadNextScene(2.7f));
            }
        }
        else
        {
            collider.GetComponent<Controls>().Freeze();
            collider.GetComponent<OnlineEvents>().endLvl();
        }
          
    }

    IEnumerator LoadNextScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
