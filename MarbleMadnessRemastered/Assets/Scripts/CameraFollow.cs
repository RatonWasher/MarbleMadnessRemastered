using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    private GameObject FollowedObject; //Object followed by the camera

    private Vector3 OffSet; //Length between camera and object followed

    void LateUpdate()
    {
        if (FollowedObject != null)
        {
            transform.position = new Vector3(FollowedObject.transform.position.x, FollowedObject.transform.position.y + 8, FollowedObject.transform.position.z - 9);
            transform.LookAt(FollowedObject.transform);
        }
        
    }

    public void setFGollowed(GameObject followed)
    {
        FollowedObject = followed; 
    }
}
