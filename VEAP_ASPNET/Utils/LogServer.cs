using Fleck;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace VEAP_ASPNET.Utils
{
    public class LogServer
    {
        private static LogServer _instance;
        public static LogServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LogServer();
                }
                return _instance;
            }
        }

        static WebSocketServer server = new WebSocketServer("ws://127.0.0.1:8181");
        static Dictionary<string, IWebSocketConnection> allsockets = new Dictionary<string, IWebSocketConnection>();

        public void Init()
        {
            if (Tool.IsInWeb)
                return;
            Debug.WriteLine("Log Server Created");
            server.Start(socket =>
            {

                socket.OnOpen = () =>
                {
                    Debug.WriteLine($"({socket.ConnectionInfo.Id})连入");
                    allsockets.Add(socket.ConnectionInfo.Id.ToString(), socket);
                    JSocketMessage msg = new JSocketMessage()
                    {
                        socketId = socket.ConnectionInfo.Id.ToString(),
                        tag = "ready"
                    };
                    socket.Send(Json.Encode(msg));
                    Debug.WriteLine(Json.Encode(msg));
                };
                socket.OnClose = () =>
                {
                    ;
                    Debug.WriteLine($"({socket.ConnectionInfo.Id})结束");
                    allsockets.Remove(socket.ConnectionInfo.Id.ToString());
                };
                socket.OnMessage = OnMessage;
            });
        }
        

        public void OnMessage(string msgStr)
        {
            dynamic msg = Json.Decode(msgStr);
            var socket = allsockets[msg.socketId];
            switch (msg.tag as string)
            {
                case "request_git":
                    string logPath = Tool.GetGitLogPath(msg.content.projectName);
                    int start = msg.content.start;
                    int count = msg.content.count;
                    string logContent = Command.GetLogContent(logPath, start, count);
                    Console.WriteLine(logContent.Length + ", " + count);
                    socket.Send(Json.Encode(new JSocketMessage() { tag = "log", content = logContent, addition = logContent.Length }));
                    break;
            }

        }
    }
}