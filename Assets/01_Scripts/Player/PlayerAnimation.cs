using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator    anim;
    private Rigidbody2D rigid;
    private PlayerMove  playerMove;
    private bool        jumping = false;


    private readonly int hashXMove      = Animator.StringToHash("xMove");
    private readonly int hashYSpeed     = Animator.StringToHash("ySpeed");
    private readonly int hashIsGround   = Animator.StringToHash("isGround");
    private readonly int hashIsJumping  = Animator.StringToHash("isJumping");
    private readonly int hashIsDashing  = Animator.StringToHash("isDashing");
    private readonly int hashDoubleJump = Animator.StringToHash("DoubleJump");


    //// Hash
    // 문자열 대채 가능한 고유 숫자
    // 무튼 헤쉬 넣으면 더 빠름

    private void Awake()
    {
        anim        = GetComponent<Animator>();
        rigid       = GetComponent<Rigidbody2D>();
        playerMove  = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        // immutable, mutable
        // immutable = 변형 불가능 = string
        // mutable = 뱐향 가능
        SetAnimVar();
    }

    private void SetAnimVar()
    {
        anim.SetFloat(hashXMove, Mathf.Abs(rigid.velocity.x));
        anim.SetFloat(hashYSpeed, rigid.velocity.y);
        anim.SetBool(hashIsGround, playerMove.isGround);
        //anim.SetBool(hashIsJumping, playerMove.isJump);
    }


    /// <summary>
    /// 플레이어 점핑 애니메이션 재생
    /// </summary>
    public void Jump()
    {
        if(jumping)
        {
            anim.SetTrigger(hashDoubleJump);
        }
        else
        {
            anim.SetBool(hashIsJumping, true);
            jumping = true;
        }
    }
    /// <summary>
    /// 플레이어 점핑 에니메이션 종료
    /// </summary>
    public void JumpEnd()
    {
        jumping = false;
        anim.SetBool(hashIsJumping, false);
    }

    public void Dash()
    {
        anim.SetBool(hashIsDashing, true);
    }

    public void DashEnd()
    {
        anim.SetBool(hashIsDashing, false);
    }
}



// 컴퓨터 분야에서 정렬과 탐색이 가장 어려운 난제
// 얼마나 효과적으로 할 것인가 대해 자료구조가 만들어짐
// 3, 5, 22, 47, 84, 95

// 대충 어떠한 것을 / 숫자 함
// 무튼 값 있으면 특정한 유일한 값이 나와야 함
