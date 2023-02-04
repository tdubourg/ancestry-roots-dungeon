using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerController : MonoBehaviour
{
    public GameObject player;
    Vector2 moveDirection = Vector2.zero;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        //InputManager.MovementActions.
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        player.transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void Move(Vector2 direction)
    {

    }
}
