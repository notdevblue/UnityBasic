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
    private int             jumpCount       = 2;
    private int             curJumpCount    = 0;
    private float           xMove;


    [SerializeField] private float          playerSpeed       = 5.0f;
    [SerializeField] private float          jumpForce         = 30.0f;
    
    
    
    [Header("��� ����")]
    [SerializeField] private GameObject     afterImagePrefab  = null;
    [SerializeField] private Transform      afterImageTrm;
    [SerializeField] private float          dashForce         = 10.0f;
    [SerializeField] private float          dashTime          = 0.2f;
                     private bool           isDash            = false;
    [SerializeField] private int            dashCount         = 1; // TODO : ����
                     private int            curDashCount      = 1;

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

        PoolManager.CreatePool<AfterImage>(afterImagePrefab, afterImageTrm, 20);
    }

    private void Start()
    {
        curJumpCount = jumpCount;
        curDashCount = dashCount;
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
        if(input.isDash && !isDash && curDashCount > 0)
        {
            Debug.Log("�뾾");
            isDash = true;
            StartCoroutine(Dash());
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
        if(isDash) { return; }

        xMove = input.xMove;
        
        if (xMove > 0)      { spriteRenderer.flipX = false; }
        else if (xMove < 0) { spriteRenderer.flipX = true; }
        rigid.velocity = new Vector2(xMove * playerSpeed, rigid.velocity.y);
    }

    private void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(groundChecker.position, 0.1f, whatIsGround);

        if(isGround)
        {
            curJumpCount = jumpCount;
            curDashCount = dashCount;
        }
    }

    private void Jump()
    {
        if(isDash) { return; }

        if (isGround && rigid.velocity.y < 0.1f)
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


    private IEnumerator Dash()
    {
        --curDashCount;

        float time       = 0;
        float afterTime  = 0;
        float targetTime = Random.Range(0.01f, 0.07f);

        Vector2 dir = spriteRenderer.flipX ? transform.right * -1 : transform.right;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir * dashForce, ForceMode2D.Impulse);
        rigid.gravityScale = 0;


        while(isDash)
        {
            time      += Time.deltaTime;
            afterTime += Time.deltaTime;

            if(afterTime >= targetTime)
            {
                AfterImage ai = PoolManager.GetItem<AfterImage>();
                ai.SetSprite(spriteRenderer.sprite, spriteRenderer.flipX, transform.position);
                targetTime = Random.Range(0.01f, 0.07f);
                afterTime = 0;
            }

            if(time >= dashTime)
            {
                isDash = false;
            }

            yield return null;
        }
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;
    }
}
