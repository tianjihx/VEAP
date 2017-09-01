using Fleck;
using NGit;
using NGit.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using VEAP_ASPNET.Utils;

namespace VEAP_ASPNET.Controllers
{
    public class BuildAPIController : ApiController
    {
        // GET: api/Build/GetProject
        public JsonResult<JReturn> GetProject(string url, string name, string systemType = "git")
        {
            string projectPath = $"{HttpRuntime.AppDomainAppPath}TempFiles/{name}";
            string logPath = $"{projectPath}/git_log.txt";
            string savePath = $"{projectPath}/src";
            Debug.Log(projectPath);
            try
            {
                Tool.CreateNewDirectory(projectPath);
                Command.GitClone(url, savePath, logPath);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message + e.StackTrace);
                return Json(JReturn.Error(e.Message));
                throw;
            }
            
            return Json(JReturn.Success);
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
                Debug.Log("创建服务器");
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
                Debug.Log("获取log");
                Thread t1 = new Thread(new ParameterizedThreadStart(TestMethod));
                t1.Start();
            }

            return "yes";
        }

        void TestMethod(object socketObj)
        {
            Debug.Log("线程创建");
            Debug.Log($"一共有{allsockets.Count}个client");
            for (int i = 0; i < 10; ++i)
            {
                Debug.Log(i);
                foreach (var socket in allsockets)
                {
                    socket.Send("hello " + i.ToString());
                }
                Thread.Sleep(500);
            }
        }
    }
}
