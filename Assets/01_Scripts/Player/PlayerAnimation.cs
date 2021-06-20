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
    // ���ڿ� ��ä ������ ���� ����
    // ��ư �콬 ������ �� ����

    private void Awake()
    {
        anim        = GetComponent<Animator>();
        rigid       = GetComponent<Rigidbody2D>();
        playerMove  = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        // immutable, mutable
        // immutable = ���� �Ұ��� = string
        // mutable = ���� ����
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
    /// �÷��̾� ���� �ִϸ��̼� ���
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
    /// �÷��̾� ���� ���ϸ��̼� ����
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



// ��ǻ�� �о߿��� ���İ� Ž���� ���� ����� ����
// �󸶳� ȿ�������� �� ���ΰ� ���� �ڷᱸ���� �������
// 3, 5, 22, 47, 84, 95

// ���� ��� ���� / ���� ��
// ��ư �� ������ Ư���� ������ ���� ���;� ��
