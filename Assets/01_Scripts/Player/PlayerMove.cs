using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D     rigid;
    private PlayerInput     input;
    private SpriteRenderer  spriteRenderer;
    private PlayerAnimation playerAnimation;
    private bool            isJump          = false;
    private bool            isDash          = false;
    private int             jumpCount       = 2;
    private int             curJumpCount    = 0;

    [SerializeField] private float playerSpeed  = 5.0f;
    [SerializeField] private float jumpForce    = 30.0f;

    [Header("Ground Check")]
    [SerializeField] private Transform   groundChecker;
    [SerializeField] private LayerMask   whatIsGround;
                     public  bool        isGround;


    // 모든 게임오브젝트는 생성된 후에 실행되지만, 그 때 컴포넌트가 없을 수 있을 가능성이 있음, 하지만 자신의 어웨이크가 실행될 때는 자신의 컴포넌트는 전부 생성이 됨
    // 결론: 자신꺼 가져오는건 확실함
    // 하지만 다른 오브젝트는 불확실
    private void Awake()
    {
        rigid           = GetComponent<Rigidbody2D>();
        input           = GetComponent<PlayerInput>();
        spriteRenderer  = GetComponent<SpriteRenderer>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Start()
    {
        curJumpCount = jumpCount;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(input.isJump)
        {
            isJump = true;
        }
        if(input.isDash)
        {
            isDash = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        CheckGround();
        Jump();
    }

    private void Move()
    {
        float xMove = input.xMove;
        
        if (xMove > 0)      spriteRenderer.flipX = false;
        else if (xMove < 0) spriteRenderer.flipX = true;

        rigid.velocity = new Vector2(xMove * playerSpeed, rigid.velocity.y);
    }

    private void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(groundChecker.position, 0.1f, whatIsGround);

        if(isGround)
        {
            curJumpCount = jumpCount;
        }
    }

    private void Jump()
    {
        if (!isJump) return;
        if (!isGround && curJumpCount <= 0) { isJump = false; return; }

        --curJumpCount;
        rigid.velocity = Vector2.zero;
        playerAnimation.Jump(); // 점프 에니메이션 재상

        rigid.AddForce(new Vector2(rigid.velocity.x, jumpForce), ForceMode2D.Impulse);
        isJump = false;


        if(isGround && rigid.velocity.y < 0.1f)
        {
            playerAnimation.JumpEnd(); // 점핑에니메이션 끝
        }
    }

}
