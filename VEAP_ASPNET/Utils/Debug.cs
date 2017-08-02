using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEAP_ASPNET.Utils
{
    public class Debug
    {
        public static void Log(object o)
        {
            System.Diagnostics.Debug.WriteLine(o);
            Console.WriteLine(o);
        }
    }
}