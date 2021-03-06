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
    
    
    
    [Header("대시 관련")]
    [SerializeField] private GameObject     afterImagePrefab  = null;
    [SerializeField] private Transform      afterImageTrm;
    [SerializeField] private float          dashForce         = 10.0f;
    [SerializeField] private float          dashTime          = 0.2f;
                     private bool           isDash            = false;
    [SerializeField] private int            dashCount         = 1; // TODO : 숙제
                     private int            curDashCount      = 1;

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
            Debug.Log("대씨");
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
            playerAnimation.JumpEnd(); // 점핑에니메이션 끝
        }
        if (!isJump) return;
        if (!isGround && curJumpCount <= 0) { isJump = false; return; }

        --curJumpCount;
        rigid.velocity = Vector2.zero;
        playerAnimation.Jump(); // 점프 에니메이션 재생

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
