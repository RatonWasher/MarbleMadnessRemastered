using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ImageManager : MonoBehaviour {

    private Image screenshot;
    private string lvlToLoad;
    private SceneHandler sceneManager;

	// Use this for initialization
	void Start () {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
        screenshot = GetComponent<Image>();
	}
	
	// Update is called once per frame
	public void lvlClicked (Sprite lvlSelected) {
        screenshot.color = new Vector4(1,1,1,1);
        screenshot.sprite = lvlSelected;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
	}

    public void editorLevelClicked(string value)
    {
        sceneManager.setSlot(value);
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void changeLevel(string lvlName)
    {
        lvlToLoad = lvlName;
    }
    
    public void startGame(){
        SceneManager.LoadScene(lvlToLoad);
    }
}
