using UnityEngine;
using System.Collections;

public class EditorController : MonoBehaviour {

    public float groundSpeed = 0.5f;
    public float airSpeed = 0.5f;

    private float xLimit = 49f;
    private float yLimit = 24f;
    private float zLimit = 49f;



    void Update () {
        CompensateTransform(Input.GetAxis("Horizontal"), 
                            0f, 
                            Input.GetAxis("Vertical"), groundSpeed);

        if (Input.GetKey("space"))
        {
            CompensateTransform(0f, 1f, 0f, airSpeed);
        }

        if (Input.GetKey("left shift"))
        {
            CompensateTransform(0f, -1f, 0f, airSpeed);
        }
    }

    void CompensateTransform(float X, float Y, float Z, float speed)
    {
        transform.position = actualizePos(transform.position);

        if (Y == 0f) //If ground move
        {

            float diffrence = transform.position.y;
            transform.Translate(speed * X, 0, Z * speed, Camera.main.transform);
            diffrence -= transform.position.y;


            transform.position = new Vector3(transform.position.x,
                                                transform.position.y + diffrence,
                                                transform.position.z);
        }
        else //If air move
        {
            float Xdifference = transform.position.x;
            float Zdifference = transform.position.z;
            transform.Translate(0f, Y * speed, 0f, Camera.main.transform);
            Xdifference -= transform.position.x;
            Zdifference -= transform.position.z;


            transform.position = new Vector3(transform.position.x + Xdifference,
            transform.position.y,
            transform.position.z + Zdifference);
        }
    }

    

    Vector3 actualizePos(Vector3 Pos)
    {
        if (Pos.x < -xLimit)
            Pos.x += 2;
        if (Pos.x > xLimit)
            Pos.x -= 2;

        if (Pos.y < -yLimit)
            Pos.y += 2;
        if (Pos.y > yLimit)
            Pos.y -= 2;

        if (Pos.z < -zLimit)
            Pos.z += 2;
        if (Pos.z > zLimit)
            Pos.z -= 2;

        return Pos;
    }

}
