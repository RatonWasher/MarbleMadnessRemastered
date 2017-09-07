using UnityEngine;
using System.Collections;

public class EditorMenu : MonoBehaviour
{

    public GameObject Menu;
    public GameObject MainCam;
    public GameObject EditorMan;
    public GameObject Player;


    private bool isMenuing = false;



    void Update()
    {
        if (!Player.activeSelf)
        {
            if (Input.GetKeyDown("p"))
                ToggleMenu();

            if (Input.GetKeyDown(KeyCode.Tab) && isMenuing)
                ToggleMenu();
        }
    }


    public void ToggleMenu()
    {
        if (!isMenuing)
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

        EditorMan.SetActive(isMenuing);


        Menu.SetActive(!isMenuing);

        isMenuing = !isMenuing;
    }
}