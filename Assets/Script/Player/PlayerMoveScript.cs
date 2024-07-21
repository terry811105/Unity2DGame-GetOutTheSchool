using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    Rigidbody2D rdBody;

    public float characterScale = 1f;

    public float speed = 5f;

    public Animator animator;

    public Transform character; // 角色的Transform组件

    private Vector2 lastMovementDirection;
    
    public bool canMove = false;
    void Start()
    {
        rdBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastMovementDirection = Vector2.down;
        animator.Play("Dw");
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
            // animator.SetFloat("Run", 0);
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


    // void UpdateAnimation(Vector2 movement)
    // {
    //     // if (movement.x != 0)
    //     // {
    //     //     transform.localScale = new Vector3(Mathf.Sign(movement.x) *characterScale, characterScale, characterScale);
    //     // }
    //     float speedValue = movement.magnitude;
    //     if (movement.x > 0)
    //     {
    //         animator.SetFloat("Right", speedValue);
    //     }
    //     else 
    //     {
    //         animator.SetFloat("Left", speedValue);
    //     }
    //     if (movement.y > 0)
    //     {
    //         animator.SetFloat("Up", speedValue);
    //     }
    //     else 
    //     {
    //         animator.SetFloat("Dw", speedValue);
    //     }

        
    // }

     public void UpdateAnimation(Vector2 movement)
    {
        if (movement.magnitude > 0.1f) // 確保有足夠的移動量
        {
            lastMovementDirection = movement.normalized;
            
            // 判斷主要移動方向
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                // 水平移動
                if (movement.x > 0)
                {
                    animator.Play("Right");
                    // transform.localScale = new Vector3(Mathf.Abs(characterScale), characterScale, characterScale);
                }
                else
                {
                    animator.Play("Left");
                    // transform.localScale = new Vector3(Mathf.Abs(characterScale), characterScale, characterScale);
                }
            }
            else
            {
                // 垂直移動
                if (movement.y > 0)
                {
                    animator.Play("Up");
                }
                else
                {
                    animator.Play("Dw");
                }
            }
        }
        else
        {
            // 沒有移動，根據最後的移動方向播放靜止動畫
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        if (Mathf.Abs(lastMovementDirection.x) > Mathf.Abs(lastMovementDirection.y))
        {
            // 水平方向
            if (lastMovementDirection.x > 0)
            {
                animator.Play("Idle_right");
                // transform.localScale = new Vector3(Mathf.Abs(characterScale), characterScale, characterScale);
            }
            else
            {
                animator.Play("Idle_left");
                // transform.localScale = new Vector3(Mathf.Abs(characterScale), characterScale, characterScale);
            }
        }
        else
        {
            // 垂直方向
            if (lastMovementDirection.y > 0)
            {
                animator.Play("Idle_up");
            }
            else
            {
                animator.Play("Idle_down");
            }
        }
    }
}
