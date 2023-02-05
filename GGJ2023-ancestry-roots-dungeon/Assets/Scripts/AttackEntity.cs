using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class AttackEntity : MonoBehaviour {
    // C# does not support string enums... 
    class AttackersTags {
        public const string Player = "Player";
        public const string Fireball = "Fireball";
        public const string Enemy = "Enemy";
    }

    public float Damage = 10;
    public float MAX_HEALTH = 100;
    public float CurrentHealth { get; private set; }

    public void Awake() {
        CurrentHealth = MAX_HEALTH;
    }

    public void TakeDamage(AttackEntity attacker) {
        var damage = attacker.Damage;
        var tagThis = this.gameObject.tag;
        var tagOther = attacker.gameObject.tag;
        if (tagThis == tagOther) {
            // just generally no friendly fire of any sort
        } else if ((tagThis == AttackersTags.Player && tagOther == AttackersTags.Fireball) || (tagThis == AttackersTags.Fireball && tagOther == AttackersTags.Player)) {
            // Fireballs do not interact with player
            return;
        }
        UpdateHealth(CurrentHealth - damage);
        // Note: fireballs' health will be set to 0 so they die as soon as they take any damage at all
        if (CurrentHealth <= 0) {
            Die();
        }
    }

    public void UpdateHealth(float newHealth) {
        CurrentHealth = newHealth;
        Debug.Log("Setting health to " + CurrentHealth);
        // potentially do more
    }
    public void Die() {
        Debug.Log("AttachEntity " + this.gameObject.tag + " dead");
        if (this.gameObject.tag == AttackersTags.Player) {
            LevelTransitioner.GetInstance().GoToLevel(Levels.GameOver);
        } else if (this.gameObject.tag == AttackersTags.Enemy || this.gameObject.tag == AttackersTags.Fireball) {
            // For now, just vanish
            Debug.Log("Vanish!");
            Destroy(this.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Entering collision");
        var other = collision.collider.gameObject;
        if (null == other) { // GO may have already been deleted
            return;
        }
        AttackManager.GetInstance().collideAttackingEntities(this.gameObject, other);
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        var other = otherCollider.gameObject;
        if (null == other) { // GO may have already been deleted
            return;
        }
        Debug.Log("Entering trigger");
        AttackManager.GetInstance().collideAttackingEntities(this.gameObject, other);
    }
}
