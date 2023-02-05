using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyType enemyType;
    private Attributes attributes;
    private GameObject Player;


    public int health;
    private float distance;
    private Vector2 direction;

    private float nextAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        attributes = new Attributes(enemyType);
        health = attributes.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (transform.position - Player.transform.position).magnitude;
        direction = (Player.transform.position - transform.position).normalized;
        if (distance > attributes.safeRange)
        {
            transform.Translate(direction * Time.deltaTime);
        }//moving towards player;
        // else if (distance < attributes.safeRange)
        // {
        //     transform.Translate(direction * -1 * Time.deltaTime);
        // }//moving away from player;
        // Basic movement;

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
