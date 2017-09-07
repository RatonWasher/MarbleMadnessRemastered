using UnityEngine;
using System.Collections;

public class EditorCameraFollow : MonoBehaviour
{

    public GameObject FollowedObject; //Object followed by the camera

    private Vector3 OffSet; //Length between camera and object followed


    void Start()
    {
        OffSet = transform.position - FollowedObject.transform.position;
    }


    void LateUpdate()
    {
        transform.position = FollowedObject.transform.position + OffSet;
    }
}