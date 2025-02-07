using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private States State
{
    get { return (States)anim.GetInteger("state"); }
    set { anim.SetInteger("state", (int)value); }
}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
{
    if (isGrounded && !Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
    {
        State = States.idle;
    }
    else if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
    {
        Run();
    }
}

    private void Run()
{
    State = States.Run;
    Vector3 dir = Vector3.zero;

    if (Input.GetKey(KeyCode.W))
    {
        dir += Vector3.up;
    }
    if (Input.GetKey(KeyCode.S))
    {
        dir += Vector3.down;
    }
    if (Input.GetKey(KeyCode.A))
    {
        dir += Vector3.left;
    }
    if (Input.GetKey(KeyCode.D))
    {
        dir += Vector3.right;
    }

    if (dir != Vector3.zero)
    {
        dir = dir.normalized;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }
    else
    {

        State = States.idle;
    }
}


    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }
}

public enum States
{
    idle,Run
}