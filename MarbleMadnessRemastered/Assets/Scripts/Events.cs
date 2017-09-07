using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour {
    public Text LoseText;


	public void gameOver(){
        gameObject.GetComponent<Controls>().loseControls();
        LoseText.text = "You lose!\nPress return to retry.";

        if(Input.GetKeyDown(KeyCode.Return)){
            Destroy(gameObject);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        
    }
}
