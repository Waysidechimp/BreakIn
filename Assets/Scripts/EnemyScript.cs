using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour
{
    [SerializeField] float speed=5;
    [SerializeField] float invulnerability = 0.5f;
    [SerializeField] TimeScript timeScript;
    [SerializeField] float additionalTime = 2f;
    private PolygonCollider2D polygon;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] List<AudioClip> clips;
    AudioClip currentClip;
    private GameObject textObject;
    private Text deathText;

    // Start is called before the first frame update
    void Start()
    {
        polygon = gameObject.GetComponent<PolygonCollider2D>();
        timeScript = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeScript>();
        currentClip = clips[Random.Range(0, clips.Count-1)];
    }

    private void Awake()
    {
        textObject = GameObject.FindGameObjectWithTag("KillCounter");
        deathText = textObject.GetComponent<Text>();

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
            addDeathCount();
            timeScript.addTime(additionalTime);
            Destroy(this.gameObject);
        }
    }

    private void addDeathCount()
    {

        int currentDeaths = int.Parse(deathText.text) + 1;
        deathText.text = currentDeaths.ToString();
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
