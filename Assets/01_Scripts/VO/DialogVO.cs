using System;
using System.Collections.Generic;

[Serializable]
public class DialogVO
{
    public int code;
    public List<TextVO> text;


    public DialogVO(int code, List<TextVO> text)
    {
        this.code = code;
        this.text = text;
    }
}