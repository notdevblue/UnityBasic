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
    private float           xMove;


    [SerializeField] private float playerSpeed  = 5.0f;
    [SerializeField] private float jumpForce    = 30.0f;
    [SerializeField] private float dashForce    = 10.0f;

    [Header("Ground Check")]
    [SerializeField] private Transform   groundChecker;
    [SerializeField] private LayerMask   whatIsGround;
                     public  bool        isGround;


    // ��� ���ӿ�����Ʈ�� ������ �Ŀ� ���������, �� �� ������Ʈ�� ���� �� ���� ���ɼ��� ����, ������ �ڽ��� �����ũ�� ����� ���� �ڽ��� ������Ʈ�� ���� ������ ��
    // ���: �ڽŲ� �������°� Ȯ����
    // ������ �ٸ� ������Ʈ�� ��Ȯ��
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
            Debug.Log("Pressed");
            isDash = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
        CheckGround();
        Jump();
        Dash();
    }

    private void Move()
    {
        xMove = input.xMove;
        
        if (xMove > 0)      spriteRenderer.flipX = false;
        else if (xMove < 0) spriteRenderer.flipX = true;
        //rigid.AddForce(new Vector2(xMove * playerSpeed, rigid.velocity.y), ForceMode2D.Impulse);
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
        if(isGround && rigid.velocity.y < 0.1f)
        {
            playerAnimation.JumpEnd(); // ���ο��ϸ��̼� ��
        }
        if (!isJump) return;
        if (!isGround && curJumpCount <= 0) { isJump = false; return; }

        --curJumpCount;
        rigid.velocity = Vector2.zero;
        playerAnimation.Jump(); // ���� ���ϸ��̼� ���

        rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isJump = false;
    }


    private void Dash()
    {
        if (Mathf.Abs(rigid.velocity.x) <= playerSpeed)
        {
            playerAnimation.DashEnd();
        }
        if (!isDash) { return; }

        playerAnimation.Dash();
        rigid.AddForce(new Vector2(xMove * dashForce, 0), ForceMode2D.Impulse);
        isDash = false;
    }
}
