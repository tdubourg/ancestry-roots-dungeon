using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public int health = 3;
    public float regenRate = 0.01f;
    public float maxPoints = 10;
    public float currentPoints = 10;
    float period = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check after 5 seconds to do something
        //if(Time.deltaTime >= 5 && currentPoints + regenRate <= maxPoints)
        //{
        //    currentPoints += regenRate;
        //    Debug.Log(currentPoints);
        //}
        
        if (period > 5)
        {
            currentPoints += regenRate;
            Debug.Log(currentPoints);
            //Do Stuff
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            var p = this.gameObject.GetComponent<PlayerController>();
            p.IsAllowInput = false;
        }
        if (collision.collider.tag == "Enemy")
        {
            if(health > 0)
            {
                health--;
                if(health == 0)
                {
                    //TODO - call onDeath()
                }    
            }
        }
    }

}
