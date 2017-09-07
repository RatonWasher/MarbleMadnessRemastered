using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevelEditor : MonoBehaviour {

    private SceneHandler sceneManager;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (sceneManager.getIsQuickplay())
        {
            collider.GetComponent<Controls>().Freeze();
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<Fade>().StartFade(1);
            StartCoroutine(ReturnToMenu(2.7f));
        }
        
        if (sceneManager.getIsEditor())
        {
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<EditorPlayModeToggle>().TogglePlayMode();
        }
        
    }

    IEnumerator ReturnToMenu(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("LevelSelect");
    }
}
