//Makes the entering gO gains speed on a specified direction

using UnityEngine;
using System.Collections;

public class SpeedUp : MonoBehaviour {

    [Range(0, 2)]public float xSpeed;
    [Range(0, 2)]public float ySpeed;
    [Range(0, 2)]public float zSpeed;


    private Renderer gORenderer; //gO Renderer is stored for the animation

    void Start()
    {
        gORenderer = GetComponent<Renderer>();
    }


    void Update() //Used for plate animation
    {
        float offset = Time.time * -1f;
        gORenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0f));
    }


	void OnTriggerEnter(Collider collider) //Used for gpeed gain
    {
        Rigidbody ColliderRB = collider.GetComponent<Rigidbody>();

        ColliderRB.velocity = 
            new Vector3(ColliderRB.velocity.x * xSpeed,
                        ColliderRB.velocity.y * ySpeed,
                        ColliderRB.velocity.z * zSpeed);
    }
}
