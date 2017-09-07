using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnlineHealth : MonoBehaviour {

    public int lifePoints;
    public Text HealthText;
    private Transform Respawn;

    void Start()
    {
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
        Respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
    }

    void Update()
    {
        HealthText.text = "Health : " + lifePoints.ToString();
        if (lifePoints == 0)
        {
            transform.position = Respawn.position;
            GetComponent<OnlineHealth>().resetHealth(3);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    public void increaseHealth(int value)
    {
        lifePoints += value;
    }

    public void decreaseHealth(int value)
    {
        lifePoints -= value;
    }

    public void resetHealth(int value)
    {
        lifePoints = value;
    }
}
