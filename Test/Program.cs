using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEAP_ASPNET.Utils;
using VEAP_ASPNET.Controllers;
using System.Web;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = "webplayer_test";
            var git_url = "https://github.com/tianjihx/webplayer_test.git";
            string projectPath =  $"{Environment.CurrentDirectory}/TempFiles/{name}";
            string logPath = $"{projectPath}/git_log.txt";
            string srcPath = $"{projectPath}/src";
            Log.Info(projectPath);
            try
            {
                //Tool.CreateNewDirectory(projectPath);
                //Command.GitClone(git_url, srcPath, logPath);
                //Log.InfoReplace(Command.GetLogContent(logPath, 0, 10240), "\r", "\n");
                Log.Info("Yes");
                LogServer.Instance.Init();
            }
            catch (Exception e)
            {
                Log.Info(e.Message);
            }
            while (true)
            {

            }
        }

        void ShowLog()
        {
            int start;
            int count;
            //Command.GetLogContent(logPath, start, count)
        }
    }

    class Log
    {
        public static void Info(object s)
        {
            Console.WriteLine(s);
        }
        
        public static void InfoReplace(string s, string a, string b)
        {
            string ss = s.ToString().Replace(a, b);
            Console.WriteLine(ss);
        }
    }
}
