using UnityEngine;
using System.Collections;

public class NopeCircular : MonoBehaviour {

    public float secondsToTurn = 5;
    public float Radius = 5;
    public bool ClockWise = true;


    private float speed;
    private float angle;   //Used for maths calcul
    private float a, b;


    void Start()
    {
        a = gameObject.transform.position.x;
        b = gameObject.transform.position.z;
        speed = (2 * Mathf.PI) / secondsToTurn;
    }

    void Update()
    {
        if (ClockWise) { angle += speed * Time.deltaTime; }
        else { angle -= speed * Time.deltaTime; }

         gameObject.transform.position = new Vector3(
             Mathf.Cos(angle) * Radius+a, 
             gameObject.transform.position.y, 
             Mathf.Sin(angle)*Radius+b
         );
    }

}
