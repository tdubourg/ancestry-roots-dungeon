using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerController : MonoBehaviour
{
    static private PlayerController instance;
    public Animator animator;

    static public PlayerController GetInstance()
    {
        if (null == instance)
        {
            Debug.Log("PlayerController.GetInstance() called before init");
        }
        return instance;
    }

    public AudioSource walkASource;
    public AudioSource lowHealthASource;
    private AttackEntity healthTracker;


    public Rigidbody2D body;
    public float moveSpeed = 2;
    public bool IsAllowInput = true;
    public float rotationSpeed = 1.0f;

    PlayerCollider _playerCollider;
    public SpriteRenderer PlayerSprite;
    public Sprite SpriteUp;
    public Sprite SpriteDown;
    public Sprite SpriteRight;
    public Sprite SpriteLeft;


    public AttackManager attackManager;

    private Vector2 lastDirectionFaced = Vector2.right;

    // private 

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        _playerCollider = this.gameObject.GetComponent<PlayerCollider>();
        instance = this;

        healthTracker = this.GetComponent<AttackEntity>();

        animator = GetComponentInChildren<Animator>();

        //InputManager.MovementActions.
        // attackManager = transform.GetComponent<AttackManager>();
    }

    private void spriteDirection(Vector2 move)
    {
        // var motionVec = new Vector2D(this.transform.X)
        var right = Vector2.Dot(move, Vector2.right);
        var left = Vector2.Dot(move, Vector2.left);
        var up = Vector2.Dot(move, Vector2.up);
        var down = Vector2.Dot(move, Vector2.down);

        if (up > 0 && up > left && up > right)
        {
            PlayerSprite.sprite = SpriteUp;
        }
        else if (down > 0 && down > left && down > right)
        {
            PlayerSprite.sprite = SpriteDown;
        }
        else if (right > 0)
        {
            PlayerSprite.sprite = SpriteRight;
        }
        else if (left > 0)
        {
            PlayerSprite.sprite = SpriteLeft;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthTracker.CurrentHealth <= healthTracker.MAX_HEALTH / 5 && !lowHealthASource.isPlaying)
        {
            lowHealthASource.Play();
        }
        else
        {
            lowHealthASource.Stop();
        }

        if (!IsAllowInput && body.velocity != Vector2.zero)
        {
            //moveSpeed = 0;
            body.velocity = Vector2.zero;
        }
        //else
        //{
        //    moveSpeed = 1;
        //}

        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            //movement.x = (-transform.right * Time.deltaTime * (moveX * -moveSpeed)).x;
            movement.x = moveX * moveSpeed;
            //Debug.Log("KeyPressA: X: " + moveX + " Y: " + moveY);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //movement.x = (transform.right * Time.deltaTime * (moveX * moveSpeed)).x;
            movement.x = moveX * moveSpeed;
            //Debug.Log("KeyPressD: X: " + moveX + " Y: " + moveY);
        }
        if (Input.GetKey(KeyCode.W))
        {
            //movement.y = (transform.up * Time.deltaTime * (moveY * moveSpeed)).y;
            movement.y = moveY * moveSpeed;
            //Debug.Log("KeyPressW: X: " + moveX + " Y: " + moveY);
        }
        if (Input.GetKey(KeyCode.S))
        {
            //movement.y = (transform.up * Time.deltaTime * (moveY * moveSpeed)).y;
            movement.y = moveY * moveSpeed;
            //Debug.Log("KeyPressS: X: " + moveX + " Y: " + moveY);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            attackManager.fire(lastDirectionFaced);
        }

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        if (movement != Vector2.zero)
        {
            if (!walkASource.isPlaying)
            {
                walkASource.Play();
            }
            lastDirectionFaced = movement;
        }
        else if (walkASource.isPlaying)
        {
            walkASource.Stop();
        }

        //movement = movement + (Vector2)(transform.position);

        //body.AddForce(movement);
        body.velocity = (movement);
        spriteDirection(movement);


        if (Input.GetKey(KeyCode.Q))
        {
            body.transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }
        if (Input.GetKey(KeyCode.E))
        {
            body.transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        }

        // Health regen temporarily disabled until it's a priority again
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     if (_playerCollider.currentPoints >= 5)
        //     {
        //         _playerCollider.currentPoints -= 5;
        //     }
        //     //TODO - hook in action process
        // }
    }

    void Move(Vector2 direction)
    {

    }
}
