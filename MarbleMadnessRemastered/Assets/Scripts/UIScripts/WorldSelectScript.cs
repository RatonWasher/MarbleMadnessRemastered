//used for handling the level selection screen

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldSelectScript : MonoBehaviour {

    public GameObject World1Panel;
    public GameObject World2Panel;
    public GameObject EditorPanel;
    public GameObject editorImage;

    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject screenshot;

    public Text worldText;
    public Sprite baseSprite;

    private SceneHandler sceneManager;
    public int currentWorld = 1;
    private int maxWorlds;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
        leftArrow.GetComponent<Image>().color = new Vector4(0.5f, 0.5f, 0.5f, 1);
        if (sceneManager.isOnline)
        {
            maxWorlds = 2;
        }
        else
        {
            maxWorlds = 3;
        }
    }

    public void leftArrowClicked()
    {
        if (currentWorld > 1)
        {
            currentWorld -= 1;
            if (currentWorld == 1)
            {
                worldText.text = "World 1";
                leftArrow.GetComponent<Image>().color = new Vector4(0.5f, 0.5f, 0.5f, 1);
                rightArrow.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                World2Panel.SetActive(false);
                World1Panel.SetActive(true);
                if (!sceneManager.isOnline)
                {
                    EditorPanel.SetActive(false);
                    editorImage.SetActive(false);
                }
                screenshot.GetComponent<Image>().sprite = baseSprite;
                foreach (Transform child in screenshot.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            if (currentWorld == 2)
            {
                worldText.text = "World 2";
                rightArrow.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                World2Panel.SetActive(true);
                World1Panel.SetActive(false);
                if (!sceneManager.isOnline)
                {
                    EditorPanel.SetActive(false);
                    editorImage.SetActive(false);
                }
                
                screenshot.GetComponent<Image>().sprite = baseSprite;
                foreach (Transform child in screenshot.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    public void rightArrowClicked()
    {
        if (currentWorld < maxWorlds)
        {
            currentWorld += 1;
            
            if (currentWorld == 2)
            {
                worldText.text = "World 2";
                if (currentWorld == maxWorlds)
                {
                    rightArrow.GetComponent<Image>().color = new Vector4(0.5f, 0.5f, 0.5f, 1);
                }
                leftArrow.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
                World1Panel.SetActive(false);
                World2Panel.SetActive(true); 
                if (!sceneManager.isOnline)
                {
                    EditorPanel.SetActive(false);
                    editorImage.SetActive(false);
                }
                
                screenshot.GetComponent<Image>().sprite = baseSprite;
                foreach (Transform child in screenshot.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
            if (currentWorld == 3)
            {
                worldText.text = "Editor's Maps";
                rightArrow.GetComponent<Image>().color = new Vector4(0.5f, 0.5f, 0.5f, 1);
                World1Panel.SetActive(false);
                World2Panel.SetActive(false);
                EditorPanel.SetActive(true);
                screenshot.GetComponent<Image>().sprite = baseSprite;
                editorImage.SetActive(true);
                foreach (Transform child in screenshot.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

    }
}
