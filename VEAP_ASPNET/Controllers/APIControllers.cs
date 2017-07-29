using Fleck;
using NGit;
using NGit.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace VEAP_ASPNET.Controllers
{
    public class GetProjectController : ApiController
    {
        // GET: api/Build/5
        public string Get(string url, string name, string systemType = "git")
        {
            string projectPath = $"{HttpRuntime.AppDomainAppPath}TempFiles/{name}";
            string logPath = $"{projectPath}/git_log.txt";
            string srcPath = $"{projectPath}/src";
            Debug.WriteLine(projectPath);
            try
            {
                if (Directory.Exists(projectPath))
                {
                    Directory.Delete(projectPath, true);
                }
                Directory.CreateDirectory(projectPath);
                if (File.Exists(logPath))
                {
                    File.Delete(logPath);
                }
                TextWriter tw = new StreamWriter(new FileStream(logPath, System.IO.FileMode.CreateNew));
                TextProgressMonitor monitor = new TextProgressMonitor(tw);
                CloneCommand cmd = Git.CloneRepository()
                    .SetURI(url)
                    .SetDirectory(srcPath)
                    .SetProgressMonitor(monitor);
                cmd.Call();
                tw.Close();
            }
            catch (Exception e)
            {
                return e.Message + e.StackTrace;
            }
            return "yes";
        }
    }

    public class TestController : ApiController
    {
        static WebSocketServer server = new WebSocketServer("ws://127.0.0.1:8181");
        static List<IWebSocketConnection> allsockets = new List<IWebSocketConnection>();
        static bool serverStart = false;

        // GET: api/Build/5
        public string Get(string cmd)
        {
            if (cmd == "0")
            {
                Debug.WriteLine("创建服务器");
                server.Start(socket =>
                {
                    allsockets.Add(socket);
                    socket.OnOpen = () => Console.WriteLine("Open!");
                    socket.OnClose = () => Console.WriteLine("Close!");
                    socket.OnMessage = message => socket.Send(message);
                });
                serverStart = true;
            }
            else if (cmd == "1")
            {
                Debug.WriteLine("获取log");
                Thread t1 = new Thread(new ParameterizedThreadStart(TestMethod));
                t1.Start();
            }

            return "yes";
        }

        void TestMethod(object socketObj)
        {
            Debug.WriteLine("线程创建");
            Debug.WriteLine($"一共有{allsockets.Count}个client");
            for (int i = 0; i < 10; ++i)
            {
                Debug.WriteLine(i);
                foreach (var socket in allsockets)
                {
                    socket.Send("hello " + i.ToString());
                }
                Thread.Sleep(500);
            }
        }
    }
}
