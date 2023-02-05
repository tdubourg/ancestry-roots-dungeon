using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyType enemyType;
    private Attributes attributes;
    private GameObject Player;
    private Animator animator;


    public int health;
    private float distance;
    private Vector2 direction;

    private float nextAttackTime;
    public GameObject sprite;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Player = GameObject.FindWithTag("Player");
        attributes = new Attributes(enemyType);
        health = attributes.maxHealth;
    }

    void LookAt2D(Vector3 targ)
    {
        Vector3 diff = targ - sprite.transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        sprite.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    // Update is called once per frame
    void Update()
    {
        distance = (transform.position - Player.transform.position).magnitude;
        direction = (Player.transform.position - transform.position).normalized;
        if (distance > attributes.safeRange)
        {
            transform.Translate(direction * Time.deltaTime);
            animator.SetFloat("movespeed", 1);


            // LookAt2D(Player.transform.position);
        }//moving towards player;
        // else if (distance < attributes.safeRange)
        // {
        //     transform.Translate(direction * -1 * Time.deltaTime);
        // }//moving away from player;
        // Basic movement;
        else
        {
            animator.SetFloat("movespeed", 0);

        }

        // if (attributes.range >= distance)
        // {
        //     //Debug.Log("Firing my laser");
        // }
    }
}
public enum EnemyType
{
    testing,
    basicMelee,
    basicStationaryMelee
}
public class Attributes
{
    public float speed;
    public float range;
    public float safeRange;
    public int maxHealth;

    // fire rate might be good to add?

    // damage too? 

    public Attributes(float speed, float range, float safeRange, int maxHealth)
    {
        this.speed = speed;
        this.safeRange = safeRange;
        this.maxHealth = maxHealth;
    }
    public Attributes(EnemyType enemyType)
    {
        if (enemyType.ToString() == "testing")
        {
            this.speed = 1; this.range = 4; this.safeRange = 2; this.maxHealth = 10;
        }
        else if (enemyType.ToString() == "basicMelee")
        {
            this.speed = 2; this.range = 0f; this.safeRange = 0.5f; this.maxHealth = 10;
        }
        else if (enemyType.ToString() == "basicStationaryMelee")
        {
            this.speed = 2; this.range = 0f; this.safeRange = 200f; this.maxHealth = 10;
        }
    }


}
