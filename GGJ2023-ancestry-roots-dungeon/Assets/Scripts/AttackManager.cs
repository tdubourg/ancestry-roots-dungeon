using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    static private AttackManager instance;
    static public AttackManager GetInstance()
    {
        if (null == instance)
        {
            Debug.Log("AttackManager.GetInstance() called before init");
        }
        return instance;
    }

    public PlayerController playerController;

    public GameObject fireBall = null;

    public enum AttackMode
    {
        None,
        Fireball
    }

    void Awake()
    {
        instance = this;
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

}
