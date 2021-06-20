using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    /// <summary>
    /// 사용자의 x축 입력값
    /// </summary>
    public float    xMove   { get; private set; }
    public bool     isJump  { get; private set; }
    public bool     isDash  { get; private set; }

    void Update()
    {
        xMove   = Input.GetAxisRaw("Horizontal");
        isJump  = Input.GetButtonDown("Jump");
        //isDash  = Input.GetButtonDown("Dash");
        isDash = Input.GetKeyDown(KeyCode.LeftShift);
    }

}
