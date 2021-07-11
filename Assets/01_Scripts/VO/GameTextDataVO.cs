using System;
using System.Collections.Generic;

[Serializable]
public class GameTextDataVO
{
    public string version;
    public List<DialogVO> list;

    public GameTextDataVO(string version, List<DialogVO> list)
    {
        this.version = version;
        this.list = list;
    }
}