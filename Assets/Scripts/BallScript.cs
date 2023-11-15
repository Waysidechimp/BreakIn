using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Located on the ball
public class BallScript : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] AudioClip ballToWall;
    [SerializeField] AudioClip ballToBrick;
    [SerializeField] AudioClip ballToEnemy;
    [SerializeField] AudioClip ballToDoor;
    AudioSource audio;


    private TrailRenderer trailRenderer;

    //If with paddle is true follow the paddle and shoot forward when
    //player clicks
    [SerializeField] PauseControl pause;

    [SerializeField] bool withPaddle = true;
    [SerializeField] GameObject paddle;

    [SerializeField] GameObject particles;
    private ParticleSystem particleSystem;
    private bool particleStatus = false;
    [SerializeField] float decreaseRateSpeed = 10f;
    [SerializeField] float increaseRateSpeed = 10f;
    [SerializeField] float maxParticleRate = 500f; // Adjust as needed


    private Rigidbody2D rb;
    [SerializeField]
    float speedInUnitPerSecond;
    //can remove serializefield
    [SerializeField]
    int wallBounce;
    [SerializeField]
    int bounceLimit = 4;
    [SerializeField]
    float maxSpeed = 10.5f;
    [SerializeField]
    float minSpeed = 9.5f;

    int recallAmount = 0;
    string currentPowerUp = "None";

    public int combo;
    public int damage;
    public bool canRecall;

    GhostBallin ghostBallin;
    [SerializeField] GameObject powerUpText;
    [SerializeField] GameObject recallText;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = particles.GetComponent<ParticleSystem>();
        ghostBallin = GetComponent<GhostBallin>();
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        wallBounce = 0;
        combo = 0;
        damage = 0;
        canRecall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (withPaddle)
            followPaddle();
        else
        {
            if (speedInUnitPerSecond > maxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }
            else if (speedInUnitPerSecond < minSpeed)
            {
                rb.velocity = rb.velocity.normalized * minSpeed;
            }
        }

        if ((Input.GetKeyDown("space")) && !withPaddle)
            usePowerUp();

        if (!pause.getGameIsPaused() && withPaddle)
            fireBall();

        if (Input.GetKeyDown("r"))
            recall();

        speedInUnitPerSecond = rb.velocity.magnitude;

        //Slowly remove particles from particle system
        if(particleStatus && particleSystem.emission.rateOverTime.constant < maxParticleRate)
        {
            //Debug.Log("Trying to increase");
            increaseParticleRate();
        }else if(!particleStatus && particleSystem.emission.rateOverTime.constant > 0)
        {
            //Debug.Log("Trying to lower");
            lowerParticleRate();
        }
    }

    void followPaddle()
    {
        Vector3 temp = new Vector3(paddle.transform.position.x, paddle.transform.position.y + 1, paddle.transform.position.z);
        transform.position = temp;
    }

    void fireBall()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up")) {

            if (Input.GetAxis("Horizontal") < -0.2) //left
            {

                rb.velocity = (Vector2.left + Vector2.up) * 7f;
                //rb.velocity = (Vector2.left ) * 7f;
            }
            else if (Input.GetAxis("Horizontal") > 0.2) //right
            {
                rb.velocity = (Vector2.right + Vector2.up) * 7f;
            }
            else //not moving go straight up
            {
                rb.velocity = Vector2.up * 10f;
            }

            //Always after the paddle is shot change it to not with paddle
            withPaddle = false;
        }
    }



    /// <summary>
    /// If comes into contact with playerBase
    /// reset volcity and relaunch the ball.
    /// </summary>
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBase")
        {
            rb.velocity = Vector3.zero;
            resetCombo();
            withPaddle = true;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided; " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Wall"))
        {
            audio.clip = ballToWall;
            audio.Play();
        }
        if (collision.gameObject.CompareTag("Brick"))
        {
            audio.clip = ballToBrick;
            audio.Play();
            addCombo();
            //float center= transform.renderer.bounds.center
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audio.clip = ballToEnemy;
            audio.Play();
            addCombo();
            Debug.Log(combo);
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            audio.clip = ballToDoor;
            audio.Play();
            addCombo();
            Debug.Log(combo);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            audio.clip = ballToWall;
            audio.Play();
            wallBounce++;
            if (wallBounce >= bounceLimit)
            {
                if (rb.velocity.y < 1 && rb.velocity.y > -1) {
                    if (rb.velocity.y >= 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 1f);
                    }
                    else {
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 1f);
                    }
                }

            }
        }
        if (collision.gameObject.tag != "Wall")
        {

            wallBounce = 0;
        }


    }

    //Power Up Stuff

    private void addCombo() {
        if (!withPaddle)
            combo++;
        if (combo >= 5) {
            damage = 1;
            trailRenderer.startColor = new Color(1f, 0.5f, 0f, 1.0f);//Orange
            trailRenderer.endColor = new Color(1f, 1f, 0f, 1.0f);//Yellow
        }
        if (combo >= 10) {
            damage = 2;
            trailRenderer.startColor = new Color(1f, 0f, 0f, 1.0f);//Red
            trailRenderer.endColor = new Color(1f, 0.5f, 0f, 1.0f);//Orange
        }
    }
    private void resetCombo() {
        combo = 0;
        damage = 0;
        trailRenderer.startColor = new Color(1f, 1f, 0f, 1.0f);//Yellow
        trailRenderer.endColor = new Color(1f, 1f, 1f, 1.0f);//White
    }

    private void recall()
    {
        if(recallAmount != 0)
        {
            withPaddle = true;
            updateRecall(-1);
        }
    }

    private void usePowerUp()
    {
        if (currentPowerUp != "None")
        {
            switch (currentPowerUp)
            {
                case "Ghost":
                    //use ghost script
                    ghostBallin.isGhostBall = true;
                    break;

                case "Shotgun":
                    //use shotgun

                    break;
                case "Combo":
                    useCombo();
                    break;
            }
            disableParticles();
            currentPowerUp = "None";
            updatePowerUpName();
        }

    }

    private void updatePowerUpName()
    {
        powerUpText.GetComponent<Text>().text = currentPowerUp;

    }

    private void enableParticles()
    {
        var main = particleSystem.main;
        switch (currentPowerUp)
        {
            case ("Ghost"):
                main.startColor = Color.white;
                break;
            case ("Combo"):
                main.startColor = Color.red;
                break;
        }

        particleStatus = true;
    }
    private void disableParticles()
    {
        particleStatus = false;
    }
    private void updateRecallText()
    {
        recallText.GetComponent<Text>().text = "Recall:\n"+recallAmount;
    }

    public void useCombo()
    {
        combo = 11;
        damage = 2;
        trailRenderer.startColor = new Color(1f, 0f, 0f, 1.0f);//Red
        trailRenderer.endColor = new Color(1f, 0.5f, 0f, 1.0f);//Orange
    }

    public void powerUpUpdate(string powerUp){
        if (currentPowerUp == "None")
        {
            currentPowerUp = powerUp;
            enableParticles();
        }
        updatePowerUpName();

    }

    public void updateRecall(int add)
    {
        recallAmount += add;
        updateRecallText();
        
    }

    //Particle Rate Lower
    private void lowerParticleRate()
    {
        var emission = particleSystem.emission;
        var currentRate = emission.rateOverTime.constant;
        var newRate = Mathf.Max(0, currentRate - Time.deltaTime * decreaseRateSpeed);
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(newRate);

    }
    //Particle Rate Increase
    private void increaseParticleRate()
    {
        var emission = particleSystem.emission;
        var currentRate = emission.rateOverTime.constant;
        var newRate = Mathf.Min(maxParticleRate, currentRate + Time.deltaTime * increaseRateSpeed);
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(newRate);

    }
}
