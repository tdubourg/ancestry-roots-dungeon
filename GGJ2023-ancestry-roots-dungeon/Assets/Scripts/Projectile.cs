using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2.0f;
    public float speed = 4.0f;
    private float deathTime = 0.0f;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        deathTime = Time.time + lifeTime;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= deathTime)
        {
            Destroy(gameObject);
        }
    }
}
