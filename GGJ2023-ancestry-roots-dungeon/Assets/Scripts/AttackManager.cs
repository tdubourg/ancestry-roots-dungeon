using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour {

    static private AttackManager instance;
    static public AttackManager GetInstance() {
        if (null == instance) {
            Debug.Log("AttackManager.GetInstance() called before init");
        }
        return instance;
    }

    public PlayerController playerController;

    public GameObject fireBall = null;

    public enum AttackMode {
        None,
        Fireball
    }

    void Awake() {
        instance = this;
    }

    public AttackMode currentAttackMode = AttackMode.Fireball;

    public void fire(Vector2 direction) {
        Debug.Log(currentAttackMode);
        switch (currentAttackMode) {
            case (AttackMode.None):
                break;
            case (AttackMode.Fireball):
                GameObject fireBallInstance = Instantiate(fireBall, transform.position, transform.rotation);
                fireBallInstance.GetComponent<Projectile>().moveDirection = direction;
                // fireBallInstance.transform.forward = direction;
                break;
            default:
                break;

        }


    }
    public void UpdatePlayerHealthUI() {

    }

    public void collideAttackingEntities(GameObject a, GameObject b) {
        Debug.Log("Exchanging damages between " + a.tag + " & " + b.tag + "?");
        var aeA = a.GetComponent<AttackEntity>();
        var aeB = b.GetComponent<AttackEntity>();
        if (null == aeA || null == aeB) {
            // Either one of those isn't meant to attach/be attacked
            Debug.Log("No need to exchange damage");
            return;
        }
        aeA.TakeDamage(aeB);
        aeB.TakeDamage(aeA);
    }

}
