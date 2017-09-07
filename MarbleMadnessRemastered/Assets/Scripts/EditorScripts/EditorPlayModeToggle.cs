using UnityEngine;
using System.Collections;
using System.IO;

public class EditorPlayModeToggle : MonoBehaviour {


    public GameObject[] EditorThings;
    public GameObject[] PlayThings;
    public GameObject PrefabSelector;


    private bool isEditing = true;



    void Update() {
        if (Input.GetKeyDown("g") && !(PrefabSelector.activeSelf))
            TogglePlayMode();
    }


    public void TogglePlayMode()
    {
        foreach (GameObject gO in EditorThings)
        {
            gO.SetActive(!isEditing);
        }

        foreach (GameObject gO in PlayThings)
        {
            if (gO.tag == "Player")
            {
                gO.transform.position = new Vector3(0, 1, 0);
                gO.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gO.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }

            if (gO.tag == "MainCamera")
                gO.transform.position = new Vector3(0, 7, -6);

            gO.SetActive(isEditing);
        }

        foreach (GameObject bloc in GameObject.FindGameObjectsWithTag("FakeBoxCollider"))
        {

            bloc.GetComponent<BoxCollider>().enabled = !isEditing;

            if (bloc.name == "EndBlock(Clone)")
                bloc.GetComponent<SphereCollider>().enabled = isEditing;
            else
                bloc.GetComponent<MeshCollider>().enabled = isEditing;
        }

        Destroy(GameObject.FindGameObjectWithTag("TempBlock"));

        isEditing = !isEditing;
    }
}
