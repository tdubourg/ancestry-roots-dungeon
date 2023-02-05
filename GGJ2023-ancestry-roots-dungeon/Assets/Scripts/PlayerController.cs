using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public float moveSpeed = 1;
    public bool IsAllowInput = true;
    public float rotationSpeed = 1.0f;

    PlayerCollider _playerCollider;

    public AttackManager attackManager;

    private Vector2 lastDirectionFaced = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        _playerCollider = this.gameObject.GetComponent<PlayerCollider>();
        //InputManager.MovementActions.
        // attackManager = transform.GetComponent<AttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (movement != Vector2.zero)
        {
            lastDirectionFaced = movement;
        }

        //movement = movement + (Vector2)(transform.position);

        //body.AddForce(movement);
        body.velocity = (movement);


        if (Input.GetKey(KeyCode.Q))
        {
            body.transform.Rotate(new Vector3(0, 0, rotationSpeed));
        }
        if (Input.GetKey(KeyCode.E))
        {
            body.transform.Rotate(new Vector3(0, 0, -rotationSpeed));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerCollider.currentPoints >= 5)
            {
                _playerCollider.currentPoints -= 5;
            }
            //TODO - hook in action process
        }
    }

    void Move(Vector2 direction)
    {

    }
}
