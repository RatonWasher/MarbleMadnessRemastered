using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour
{
    public GameObject Builder;
    public Material PreviewMaterial;

    private Material OriginalMaterial;
    private GameObject Temp;
    private float Rotation = 0f;

    private float xLimit = 49f;
    private float yLimit = 24f;
    private float zLimit = 49f;


    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        OriginalMaterial = Builder.GetComponent<Renderer>().sharedMaterial;
    }


    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0.1f || Input.GetAxis("Mouse ScrollWheel") == -0.1f)
        {
            Rotation += Input.GetAxis("Mouse ScrollWheel") * 900f;
            Debug.Log(Rotation);

            if (Rotation == 360f || Rotation == -360f)
                Rotation = 0;
        }

        Ray rayOne = Camera.main.ScreenPointToRay(
                new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));

        RaycastHit hitOne;

        if (Physics.Raycast(rayOne, out hitOne, 2000))
        {

            if (hitOne.transform.gameObject != Temp)
            {
                Destroy(Temp);
                // it's better to find the center of the face like this:
                Vector3 position = new Vector3(
                    Mathf.Round(hitOne.transform.position.x),
                    Mathf.Round(hitOne.transform.position.y),
                    Mathf.Round(hitOne.transform.position.z))
                    + hitOne.normal;

                Builder.GetComponent<BoxCollider>().enabled = false;
                Builder.GetComponent<Renderer>().material = PreviewMaterial;

                if (Builder.name.Substring(0, 7) == "RampLow")
                    {
                    position.y -= 0.4122f;
                }
                Temp = Instantiate(Builder, position, Quaternion.Euler(0, Rotation, 0)) as GameObject;
                Temp.tag = "TempBlock";

                Builder.GetComponent<Renderer>().material = OriginalMaterial;
                Builder.GetComponent<BoxCollider>().enabled = true;


            }
            else
            {
                Builder.GetComponent<Renderer>().material = OriginalMaterial;
            }

        }
        else
        {
            Destroy(Temp);
            Builder.GetComponent<Renderer>().material = OriginalMaterial;
        }


        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(
                new Vector3(Screen.width / 2, Screen.height / 2, 0.0f));

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2000))
            {
                // it's better to find the center of the face like this:
                Vector3 position = new Vector3(
                    Mathf.Round(hit.transform.position.x),
                    Mathf.Round(hit.transform.position.y),
                    Mathf.Round(hit.transform.position.z))
                    + hit.normal;

                // calculate the rotation to create the object aligned with the face normal:
                // Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                // Debug.Log(rotation);
                // create the object at the face center, and perpendicular to it:

                if (Input.GetMouseButtonDown(0)) //Left click
                {
                    if (checkLimitsEditor(position))
                    {
                        //If no OriginBlock and Space is empty so we can put a block here
                        if (!(position.x == 0 && position.z == 0 && position.y > 0 && position.y < 3) &&
                        (Physics.OverlapSphere(position, 0.1f).Length == 0))
                        {
                            if (Builder.name.Substring(0, 7) == "RampLow")
                            {
                                position.y -= 0.4122f;
                            }

                            Builder.GetComponent<Renderer>().material = OriginalMaterial;
                            Builder.GetComponent<BoxCollider>().enabled = true;
                            GameObject Placement = Instantiate(Builder, position, Quaternion.Euler(0, Rotation, 0)) as GameObject;
                        }
                    }
                }
                else if (Input.GetMouseButtonDown(1)) //Right click
                {
                    if (checkLimitsEditor(position))
                    {
                        if (hit.transform.tag != "OriginBlock")
                        {
                            Destroy(hit.transform.gameObject);
                        }
                    }
                }
                else //Middle click
                {
                    string path = hit.transform.name;
                    path = path.Remove(path.Length - 7);

                    Builder = (GameObject)Resources.Load(path, typeof(GameObject));
                    OriginalMaterial = Builder.GetComponent<Renderer>().sharedMaterial;
                }
            }
        }


    }

    bool checkLimitsEditor(Vector3 Pos)
    {
        if (Pos.x < -xLimit || Pos.x > xLimit ||
            Pos.y < -yLimit || Pos.y > yLimit ||
            Pos.z < -zLimit || Pos.z > zLimit)
        {
            return false;
        }

        return true;
    }


    public void SetBuilder(GameObject newPrefab)
    {
        Builder = newPrefab;
        OriginalMaterial = newPrefab.GetComponent<Renderer>().sharedMaterial;
    }

}