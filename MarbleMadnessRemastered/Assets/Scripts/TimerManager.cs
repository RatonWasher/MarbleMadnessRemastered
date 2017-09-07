using UnityEngine;
using System.Collections;

using System; //Math.Round
using UnityEngine.UI; //Displaying the text


public class TimerManager : MonoBehaviour
{

    public float Duration; //Time remaining on the timer in seconds (to change depending on the level)
    public Text DurationText; //Timer's text object
    private SceneHandler sceneManager;

    private bool timerStop = false;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
    }


    void Update()
    {
        
        //Updating the timer's display
        if (!timerStop) {
            Duration -= Time.deltaTime;
            sceneManager.setTimerText("Time: " + Mathf.Round(Duration).ToString());
        }


        //Death by timer
        if (Duration <= 0.2f)
        {
            stopTimer();
            StartCoroutine(gameObject.GetComponent<Health>().gameOver());
        }
    }

    public void stopTimer() {
        timerStop = true;
    }

}
