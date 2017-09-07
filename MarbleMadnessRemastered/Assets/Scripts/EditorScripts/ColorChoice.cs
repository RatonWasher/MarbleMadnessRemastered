using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorChoice : MonoBehaviour {

    public Sprite SelectedSprite;



    void Start()
    {

        GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach (GameObject Choice in GameObject.FindGameObjectsWithTag("ColorChoice"))
            {
                Choice.SetActive(false);
            }

            foreach (Transform ColorChoice in GameObject.Find("Canvas/EditorMode/PrefabSelector/Grid").transform)
                ColorChoice.GetComponent<Image>().sprite = null;

            GameObject.Find("Canvas/EditorMode/PrefabSelector/"+name + "Choices").SetActive(true);

            GetComponent<Image>().sprite = SelectedSprite;
        });
    }
}
