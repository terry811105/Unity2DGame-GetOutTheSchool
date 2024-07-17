using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    Rigidbody2D rdBody;

    public float speed = 5f;

    public Animator animator;

    public Transform character; // 角色的Transform组件
    
    public bool canMove = false;
    void Start()
    {
        rdBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (canMove)
        {
           Move();
        } 
        else 
        {
            // 如果不能移動，確保角色停止
            rdBody.velocity = Vector2.zero;
            animator.SetFloat("Run", 0);
        }
        
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rdBody.velocity = speed * movement;

        UpdateAnimation(movement);
    }

    void ControlMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal) * 15f, 15f, 15f);
        }

        Vector2 movement = new Vector2(horizontal, vertical);
        rdBody.velocity = speed * movement;

        float speedValue = movement.magnitude;
        animator.SetFloat("Run", speedValue);
    }

    void UpdateAnimation(Vector2 movement)
    {
        if (movement.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movement.x) *15f, 15f, 15f);
        }

        float speedValue = movement.magnitude;
        // animator.SetFloat("Run", speedValue);
    }
}
