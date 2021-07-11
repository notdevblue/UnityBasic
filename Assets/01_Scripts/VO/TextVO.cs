using System;

[Serializable]
public class TextVO
{
    public int icon;
    public string msg;

    public TextVO(int icon, string msg)
    {
        this.icon = icon;
        this.msg = msg;
    }
}
