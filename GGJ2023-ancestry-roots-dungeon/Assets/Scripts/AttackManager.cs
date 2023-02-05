using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public PlayerController playerController;


    public GameObject fireBall;

    public enum AttackMode
    {
        None,
        Fireball
    }

    public AttackMode currentAttackMode;

    public void fire(Vector2 direction)
    {
        GameObject fireBallInstance = Instantiate(fireBall);
    }

    private void Update()
    {
    }
}
