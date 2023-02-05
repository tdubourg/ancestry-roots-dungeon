using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    public Rigidbody2D player;
    Vector2 moveDirection = Vector2.zero;
    public float speed = 1;

    public bool IsAllowInput = true;

    private AttackManager attackManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        //InputManager.MovementActions.
        attackManager = transform.GetComponent<AttackManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAllowInput)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
            //player.Cast(moveDirection, )
            var parent = player.transform.parent;
            parent.transform.Translate(moveDirection * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.Q))
            {
                parent.transform.Rotate(new Vector3(0, 0, 1));
            }
            if (Input.GetKey(KeyCode.E))
            {
                parent.transform.Rotate(new Vector3(0, 0, -1));
            }
            if (Input.GetKey(KeyCode.Space))
            {
                attackManager.fire(moveDirection);
            }


        }
    }

    void Move(Vector2 direction)
    {

    }
}
