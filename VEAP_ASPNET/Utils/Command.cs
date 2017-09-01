using NGit;
using NGit.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace VEAP_ASPNET.Utils
{
    public class Command
    {
        public static void GitClone(string url, string savePath, string logPath)
        {
            try
            {
                //TextWriter tw = new StreamWriter(new FileStream(logPath, System.IO.FileMode.Create), Encoding.UTF8);
                //TextProgressMonitor monitor = new TextProgressMonitor(tw);
                //CloneCommand cmd = Git.CloneRepository()
                //    .SetURI(url)
                //    .SetDirectory(savePath)
                //    .SetProgressMonitor(monitor);
                //cmd.Call();
                //cmd.GetRepository().Close();
                //tw.Close();

                Process p = new Process();
                p.StartInfo.FileName = "C:/Program Files/Git/bin/git.exe";
                p.StartInfo.Arguments = $"clone {url} \"{savePath}\"";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                string errorLog = p.StandardError.ReadToEnd();
                string outputLog = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
                Debug.Log(errorLog);
                Debug.Log(outputLog);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetLogContent(string logPath, long start = 0, int count = 0)
        {
            if (!File.Exists(logPath))
                throw new Exception("未找到Log文件" + logPath);
            try
            {
                FileStream fs = new FileStream(logPath, System.IO.FileMode.Open);
                fs.Seek(start, SeekOrigin.Begin);
                //如果count为0则读取所有
                if (count == 0)
                {
                    StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                    return sr.ReadToEnd();
                }
                //读取start到count的所有文本
                byte[] buffer = new byte[count + 1];
                fs.Read(buffer, 0, count);
                char[] chars = Encoding.UTF8.GetChars(buffer);
                //int readCount = chars.Length;
                string content = new string(chars);
                fs.Close();
                return content;
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
                throw e;
            }
            
        }
    }
}