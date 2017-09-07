using UnityEngine;
using System.Collections;

public class EditorPrefabSelection : MonoBehaviour
{

    public GameObject EditorMan;
    public GameObject MainCam;
    public GameObject SelectionMenu;
    public GameObject Player;

    private bool isSelecting = false; //Used to know if we are in editor selection menu



    void Update()
    {
        //If the player is not activated -> if we are in editor mode -> we can open selection
        if (!Player.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                ToggleSelection();

            if (Input.GetKeyDown("p") && isSelecting)
                ToggleSelection();
        }
    }


    public void ToggleSelection()
    {
        if (!isSelecting)
        {
            MainCam.transform.parent = null;
            MainCam.GetComponent<MouseLook>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            MainCam.transform.parent = EditorMan.transform;
            MainCam.GetComponent<MouseLook>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        EditorMan.SetActive(isSelecting);

        SelectionMenu.SetActive(!isSelecting);

        isSelecting = !isSelecting;
    }

}