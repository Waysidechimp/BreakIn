using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    [SerializeField] float speed=5;
    [SerializeField] float invulnerability = 0.5f;
    private PolygonCollider2D polygon;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] List<AudioClip> clips;
    private GameObject playerBase;
    AudioClip currentClip;
    private Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        playerBase = GameObject.FindGameObjectWithTag("PlayerBase");
        polygon = gameObject.GetComponent<PolygonCollider2D>();
        currentClip = clips[Random.Range(0, clips.Count-1)];
        scoreText = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<Text>();
    }



    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0,-speed)*Time.deltaTime);

        if(!polygon.enabled)
        iFrame();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Paddle") {
            AudioSource.PlayClipAtPoint(currentClip, transform.position, 1);
            ParticleSystem debris = Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
            debris.transform.position = new Vector3(debris.transform.position.x + 0.5f, debris.transform.position.y - 0.25f, debris.transform.position.z);
            playerBase.GetComponent<PauseControl>().addDeathToll();
            addScore(1);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "Paddle")
        {
            AudioSource.PlayClipAtPoint(currentClip, transform.position, 1);
            ParticleSystem debris = Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
            debris.transform.position = new Vector3(debris.transform.position.x + 0.5f, debris.transform.position.y - 0.25f, debris.transform.position.z);
            playerBase.GetComponent<PauseControl>().addDeathToll();
            addScore(1);
            Destroy(this.gameObject);
        }
    }

    private void addScore (int additionalScore)
    {
        int newScore = int.Parse(scoreText.text) + additionalScore;
        scoreText.text = newScore.ToString();
    }


    private void iFrame()
    {
        invulnerability -= Time.deltaTime;

        if(invulnerability <= 0)
        {
            polygon.enabled = true;
        }
    }

}
