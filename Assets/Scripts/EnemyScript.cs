using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    float speed=5;
    [SerializeField] float invulnerability = 0.5f;
    private PolygonCollider2D polygon;
    [SerializeField] ParticleSystem explosion;


    // Start is called before the first frame update
    void Start()
    {
        polygon = gameObject.GetComponent<PolygonCollider2D>();
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
        if (collision.gameObject.tag == "Ball") {
            ParticleSystem debris = Instantiate(explosion, this.gameObject.transform.position, Quaternion.identity);
            debris.transform.position = new Vector3(debris.transform.position.x + 0.5f, debris.transform.position.y - 0.25f, debris.transform.position.z);

            Destroy(this.gameObject);
        }
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
