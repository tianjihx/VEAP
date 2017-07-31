using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace VEAP_ASPNET.Utils
{
    public class Tool
    {

        public static void CreateNewDirectory(string dirPath)
        {
            try
            {
                if (Directory.Exists(dirPath))
                {
                    Directory.Delete(dirPath, true);
                }
                Directory.CreateDirectory(dirPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string GetGitLogPath(string projectName)
        {
            string baseDir;
            if (IsInWeb)
                baseDir = HttpRuntime.AppDomainAppPath;
            else
                baseDir = Environment.CurrentDirectory + "/";
            return $"{baseDir}TempFiles/{projectName}/git_log.txt";
        }

        public static string GetUnityLogPath(string projectName)
        {
            string baseDir;
            if (IsInWeb)
                baseDir = HttpRuntime.AppDomainAppPath;
            else
                baseDir = Environment.CurrentDirectory + "/";
            return $"{baseDir}TempFiles/{projectName}/unity_log.txt";
        }

        public static bool IsInWeb
        {
            get
            {
                return HttpContext.Current != null;
            }
        }
    }
}