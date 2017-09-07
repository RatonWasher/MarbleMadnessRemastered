using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EditorMenuUI : MonoBehaviour
{

    private SceneHandler sceneManager;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
    }

    public void buttonClicked(string value)
    {
        sceneManager.setSlot(value);
        SceneManager.LoadScene("Editor");
    }
}
