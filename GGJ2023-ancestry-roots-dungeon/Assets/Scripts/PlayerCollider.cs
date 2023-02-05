using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.
using System;


public class PlayerCollider : MonoBehaviour
{
    // Health stuff is now handled in the PlayerHealthUI behavior
    // const float MAX_HEALTH = 100;
    // public float health = MAX_HEALTH;
    // public float healthUIProgressSpeed = 0.2f;
    // private float healthUI;
    // public float regenRate = 0.01f;
    // public float maxPoints = 10;
    // public float currentPoints = 10;
    // float period = 0;
    // Start is called before the first frame update
    void Start()
    {
        // healthUI = health;
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
        
        // if (period > 5)
        // {
        //     currentPoints += regenRate;
        //     //Do Stuff
        //     period = 0;
        // }
        // period += UnityEngine.Time.deltaTime;

    //     // This is horribly hacky, not in the right place, we'll move it later if we even get to it :D 
    //     if (Math.Abs(healthUI - health) > 0.01) {
    //         // all the extra variables are when we need debug logs
    //         var sign = (health - healthUI) / Math.Abs(healthUI - health);
    //         var healthUIChange = healthUIProgressSpeed * sign;
    //         var newHealthUI = healthUI + healthUIChange;
    //         healthUI = Math.Max(newHealthUI, health);
    //         Healthbar.fillAmount = healthUI / MAX_HEALTH;
    //     }
    // }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            var p = this.gameObject.GetComponent<PlayerController>();
            p.IsAllowInput = false;
        }
        if (collision.collider.tag == "Enemy")
        {
            // This is now handled in the AttackEntity mono behavior


            // AttackManager.GetInstance().collideAttackingEntities(this.gameObject, collision.collider.gameObject);
            // if (health > 0)
            // {
            //     health *= 0.8f; // TODO use the actual damage from the enemy??
            //     // var HealthContainer = HealthbarGO.GetComponent<UI_Health>();
            //     // if (HealthContainer.HealthContainer.Count > 0)
            //     // {
            //     //     HealthContainer.LoseHealth();
            //     //     HealthContainer.HealthContainer.RemoveAt(0);
            //     // }
            //     if(health == 0)
            //     {
            //         //TODO - call onDeath()
            //     }    
            }
        }
    }

}
