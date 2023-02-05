using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject fireBall = null;

    public enum AttackMode
    {
        None,
        Fireball
    }

    public AttackMode currentAttackMode = AttackMode.Fireball;



    public void fire(Vector2 direction)
    {
        Debug.Log(currentAttackMode);
        switch (currentAttackMode)
        {
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

    private void Update()
    {
        currentAttackMode = AttackMode.Fireball;
    }
}
