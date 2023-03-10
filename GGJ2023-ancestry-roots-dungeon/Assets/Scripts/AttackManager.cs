using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip fireBallSound;
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
        if (null != instance) {
            return;
        }
        instance = this;
        audioSource = GetComponent<AudioSource>();
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
                if (fireBallSound)
                {
                    audioSource.time = 2f;
                    audioSource.PlayOneShot(fireBallSound, 1.0f);
                }
                else
                    Debug.Log("fireball sound not loaded!");
                // fireBallInstance.transform.forward = direction;
                break;
            default:
                break;

        }


    }
    public void UpdatePlayerHealthUI()
    {

    }

    public void collideAttackingEntities(GameObject a, GameObject b)
    {
        Debug.Log("Exchanging damages between " + a.tag + " & " + b.tag + "?");
        var aeA = a.GetComponent<AttackEntity>();
        var aeB = b.GetComponent<AttackEntity>();
        if (null == aeA || null == aeB)
        {
            // Either one of those isn't meant to attach/be attacked
            Debug.Log("No need to exchange damage");
            return;
        }
        aeA.TakeDamage(aeB);
        aeB.TakeDamage(aeA);
    }

}
