using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 1.0f;
    public float speed = 0.0f;
    private float deathTime = 0.0f;
    private Rigidbody2D rb;

    public Vector2 moveDirection = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        deathTime = Time.time + lifeTime;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDirection * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = rb.forward * speed;
        Debug.Log(transform.forward);

        if (Time.time >= deathTime)
        {
            Destroy(gameObject);
        }

        // Debug.Log(rb.velocity);
    }
}
