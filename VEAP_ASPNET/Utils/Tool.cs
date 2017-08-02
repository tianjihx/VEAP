using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

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

        internal static object QueryEX(string sql)
        {
            throw new NotImplementedException();
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

        public static string DapperRows2JsonString(IEnumerable<dynamic> dapperRows)
        {
            var result = dapperRows as IEnumerable<IDictionary<string, object>>;
            var middle = result.Select(r => r.Distinct().ToDictionary(d => d.Key, d => d.Value));
            return Json.Encode(middle);
        }

        //public static dynamic DapperRow2Json(dynamic dapperObject)
        //{
        //    string jsonString = (dapperObject as object).ToString()
        //        .Replace("DapperRow, ", "")
        //        .Replace("=", ":")
        //        .Replace("NULL", "null");
        //    Debug.Log(jsonString);
        //    return Json.Decode(jsonString);
        //}

        //public static string DapperRows2JsonArrayString(dynamic[] dapperRows)
        //{
        //    List<dynamic> jsonArray = new List<dynamic>();
        //    foreach (var row in dapperRows)
        //    {
        //        jsonArray.Add(DapperRow2Json(row));
        //    }
        //    return Json.Encode(jsonArray);
        //}
    }
}