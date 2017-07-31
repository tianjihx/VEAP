using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class JReturn
{
    public bool status;
    public string errMsg;

    public static JReturn Success = new JReturn()
    {
        status = true,
        errMsg = ""
    };

    public static JReturn Error(string msg)
    {
        JReturn rtn = new JReturn()
        {
            status = false,
            errMsg = msg
        };
        return rtn;
    }
}

public class JSocketMessage
{
    public string socketId;
    public string tag;
    public string content;
    public object addition;
}
