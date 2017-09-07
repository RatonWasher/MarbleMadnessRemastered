using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    private float LifePoints;
    private float MaxLife = 0f;
    private float InvincibleTime = 2f;
    private float InvincibleTimeMax = 0f;
    private float isFallingHigh = 0f;

    private bool isPlayer;
    private bool isInvincible;

    private bool isCatched;

    private SceneHandler sceneManager;
    private Rigidbody RB;
    private Color originalColor;



    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneHandler>();
        if (tag == "Player")
        {
            isPlayer = true;
            LifePoints = 100f;
            InvincibleTimeMax += InvincibleTime;

            RB = GetComponent<Rigidbody>();
            originalColor = GetComponent<Renderer>().material.color;
        }
        else
        {
            isPlayer = false;
            LifePoints = 60f;
        }

        MaxLife += LifePoints;
    }


    void Update()
    {
        if (isPlayer)
        {
            //Calculating fall damages
            if (RB.velocity.y < -15f && RB.velocity.y > -20f)
            {
                isFallingHigh = 20f;
            }
            else if (RB.velocity.y < -20f && RB.velocity.y > -25f)
            {
                isFallingHigh = 30f;
            }
            else if (RB.velocity.y < -25f && RB.velocity.y > -30f)
            {
                isFallingHigh = 50f;
            }

            //Invulnerability after being hit
            if (isInvincible)
            {

                InvincibleTime -= Time.deltaTime;

                if (InvincibleTime <= 0f)
                {
                    isInvincible = false;
                    InvincibleTime = InvincibleTimeMax;
                }
            }
            if (LifePoints < 0)
            {
                LifePoints = 0;
            }
            sceneManager.setHealthText("HP : " + Mathf.Round(LifePoints).ToString());
        }
    }


    void OnTriggerEnter(Collider collider)
    {

        if (isPlayer && !isInvincible)
        {
            if (collider.transform.tag == "Enemy") //Collision with an enemy
            {
                TakeDamage(35f);
                collider.GetComponent<Health>().TakeDamage(35f);
                isInvincible = true;
                StartCoroutine(Flash());
            }
            else if (collider.transform.tag == "Thunder") //Thunder struck
            {
                TakeDamage(50f);
                isInvincible = true;
                StartCoroutine(Flash());
            }

            if (isFallingHigh > 0f) //Taking fall damages
            {
                //No fall damage if limits or special objects were hit
                if (!(collider.transform.tag == "Limits" || collider.transform.tag == "NoFall"))
                {
                    TakeDamage(isFallingHigh);
                }
                isFallingHigh = 0f;
            }
        }
    }


    public void TakeDamage(float damage)
    {
        LifePoints -= damage;

        if (LifePoints <= 0)
        {
            if (!sceneManager.isOnline)
            {
                DieTotal();
            }
            else
            {
                LifePoints = 100f;
                Die();
            }
        }
    }


    public void Die()
    {
        GameObject.FindGameObjectWithTag("GlobalScripts").
            GetComponent<Respawn>().GoRespawn(transform);
    }


    public void DieTotal()
    {
        if (isPlayer)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator gameOver()
    {
        
        GetComponent<Controls>().Freeze();
        sceneManager.showGameOver();
        yield return new WaitForSeconds(2f);
        DieTotal();
    }

    IEnumerator Flash()
    {
        for (int x = 0; x < InvincibleTime * 20; x++)
        {
            GetComponent<Renderer>().material.color = Color.black;
            yield return new WaitForSeconds(.1f);
            GetComponent<Renderer>().material.color = originalColor;
            yield return new WaitForSeconds(.1f);
        }
    }

    public bool getIsCatched()
    {
        return isCatched;
    }

    public void setIsCatched(bool newset)
    {
        isCatched = newset;
    }
}
