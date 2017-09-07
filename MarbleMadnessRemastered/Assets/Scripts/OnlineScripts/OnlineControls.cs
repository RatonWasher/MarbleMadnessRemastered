using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class OnlineControls : NetworkBehaviour {

    public float Speed;
    public float maxAngularVelocity;
    private bool isControllable = true;

    private Rigidbody BallRB;
    private Vector3 RespawnPos;


    IEnumerator Start()
    {
        BallRB = GetComponent<Rigidbody>();
        BallRB.maxAngularVelocity = maxAngularVelocity;
        if (isLocalPlayer)
        {
            RespawnPos = transform.position;
            yield return new WaitForEndOfFrame();
            GameObject cam = GameObject.Find("Main Camera");
            cam.GetComponent<OnlineCamera>().enabled = true;
            cam.GetComponent<OnlineCamera>().FollowedObject = this.gameObject; 
            
        }
    }


    void FixedUpdate()
    {
        //check if its the local machine
        if (isLocalPlayer)
        {
            //Movement
            if (isControllable)
            {
                
                float xAxis = Input.GetAxis("Horizontal");
                float zAxis = Input.GetAxis("Vertical");

                Vector3 Direction = new Vector3(xAxis, 0.0f, zAxis);
                BallRB.AddForce(Direction * Speed);

            }
        }
        

    }


    public void loseControls()
    {
        isControllable = false;
    }

    public void stopBall()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void gainControls()
    {
        isControllable = true;
    }

    public void Immobilize()
    {
        isControllable = false;
        BallRB.velocity = Vector3.zero;
        BallRB.angularVelocity = Vector3.zero;
    }

    public void Immobilize(Vector3 pos)
    {
        isControllable = false;
        BallRB.velocity = Vector3.zero;
        BallRB.angularVelocity = Vector3.zero;
        BallRB.transform.position = pos;
    }

    public void Freeze()
    {
        BallRB.constraints = RigidbodyConstraints.FreezePositionX |
                             RigidbodyConstraints.FreezePositionZ |
                             RigidbodyConstraints.FreezePositionY;

        BallRB.constraints = RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationZ |
                             RigidbodyConstraints.FreezeRotationY;

        BallRB.angularVelocity = Vector3.zero;
        isControllable = false;
    }

    public void Release()
    {
        isControllable = true;
        BallRB.constraints = RigidbodyConstraints.None;
    }

    public Vector3 GetRespawnPos()
    {
        return RespawnPos;
    }

    public void SetRespawnPos(Vector3 NewPos)
    {
        RespawnPos = NewPos;
    }
}
