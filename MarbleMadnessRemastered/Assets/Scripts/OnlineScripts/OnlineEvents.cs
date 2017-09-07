using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class OnlineEvents : NetworkBehaviour {

    private SceneHandler sceneManager;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
    }

    public void endLvl()
    {
        if (isLocalPlayer)
        {
            sceneManager.setGameOver(false);
        }
        else
        {
            sceneManager.setGameOver(true);
        }

    }
}
