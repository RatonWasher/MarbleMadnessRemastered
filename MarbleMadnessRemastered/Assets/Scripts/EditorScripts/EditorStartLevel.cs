using UnityEngine;
using System.Collections;

public class EditorStartLevel : MonoBehaviour {

    private SceneHandler sceneManager;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
        GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<SaveMap>().LoadEditorMap(sceneManager.slotUsed);
        if (sceneManager.getIsQuickplay())
        {
            
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<EditorPlayModeToggle>().TogglePlayMode();
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<EditorPlayModeToggle>().enabled = false;
            GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<EditorMenu>().enabled = false;
            
        }

    }
	
}
