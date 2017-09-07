using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Controls : NetworkBehaviour
{

    public float Speed;
    public float maxAngularVelocity;
    private bool isControllable = true;

    private Rigidbody BallRB;
    private Vector3 RespawnPos;
    private SceneHandler sceneManager;


    void Start()
    {
        BallRB = GetComponent<Rigidbody>();
        BallRB.maxAngularVelocity = maxAngularVelocity;
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
        StartCoroutine(initializeCamera(0.1f));
        RespawnPos = transform.position;

    }

    IEnumerator initializeCamera(float waitTime)
    {
        if (sceneManager.isOnline)
        {
            if (isLocalPlayer)
            {
                RespawnPos = transform.position;
                yield return new WaitForSeconds(waitTime);
                GameObject cam = GameObject.Find("Main Camera");
                GameObject onlineCam = GameObject.Find  ("OnlineCam");
                cam.SetActive(false);
                onlineCam.GetComponent<OnlineCamera>().enabled = true;
                
                onlineCam.GetComponent<OnlineCamera>().FollowedObject = gameObject;
            }
        }
        else
        {
            if (sceneManager.getActiveScene() != 5)
            {
                RespawnPos = transform.position;
                GameObject cam = GameObject.Find("Main Camera");
                GameObject onlineCam = GameObject.Find("Online Cam");
                CameraFollow camFollow = cam.GetComponent<CameraFollow>();
                camFollow.enabled = true;

                yield return new WaitForSeconds(waitTime);
                camFollow.setFGollowed(gameObject);
            }
            
        }
    }

    void FixedUpdate()
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


    public void loseControls(){
        isControllable = false;
    }

    public void gainControls(){
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