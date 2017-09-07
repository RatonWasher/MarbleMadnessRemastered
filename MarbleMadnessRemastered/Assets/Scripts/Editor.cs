using UnityEngine;
using System.Collections;

public class Editor : MonoBehaviour {

    //private GameObject Player;
    public GameObject cam;
    public GameObject player;
    public GameObject limits;

    private float xLimit = 49f;
    private float yLimit = 24f;
    private float zLimit = 49f;

    void Update () {
        if (Input.GetKeyDown("g"))
        {
            player.SetActive(true);
            cam.SetActive(true);
            limits.SetActive(true);


            foreach (GameObject gO in GameObject.FindGameObjectsWithTag("GameController"))
            {
                gO.SetActive(false);
            }

            foreach (GameObject bloc in GameObject.FindGameObjectsWithTag("FakeBoxCollider")){
                bloc.GetComponent<MeshCollider>().enabled = true;
                bloc.GetComponent<BoxCollider>().enabled = false;
            }

            Destroy(GameObject.FindGameObjectWithTag("TempBlock"));
            GameObject.FindGameObjectWithTag("Canvas").SetActive(false);

        }
	}


    public bool checkLimitsEditor(Vector3 Pos)
    {
        Debug.Log(Pos.y);
        if (Pos.x < -xLimit+1 || Pos.x > xLimit-1 ||
            Pos.y < -yLimit+1 || Pos.y > yLimit-1 ||
            Pos.z < -zLimit+1 || Pos.z > zLimit-1)
        {
            return false;
        }

        return true;
    }


    public bool checkLimitsEditor(Vector3 Pos, Vector3 Directions)
    {
        if ((Directions.x < 0 && Pos.x < -xLimit) || (Directions.x > 0 && Pos.x > xLimit) ||
           (Directions.y < 0 && Pos.y < -yLimit) || (Directions.y > 0 && Pos.y > yLimit) ||
           (Directions.z < 0 && Pos.z < -zLimit) || (Directions.z > 0 && Pos.z > zLimit))
        {
            return false;
        }

        return true;
    }

}
