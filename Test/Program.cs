using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VEAP_ASPNET.Utils;
using VEAP_ASPNET.Controllers;
using System.Web;
using System.Threading;
using VEAP_ASPNET.Models;
using System.Data.SQLite;
using System.Data;
using Dapper;
using System.Dynamic;

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

                var projects = DAO.GetProjectByUserId(1);
                dynamic JsonData = new ExpandoObject();
                JsonData.Projects = Tool.DapperRows2JsonArrayString(projects);
                Debug.Log(Tool.DapperRows2JsonArrayString(projects));
            }
            catch (Exception e)
            {
                Log.Info(e.Message);
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
