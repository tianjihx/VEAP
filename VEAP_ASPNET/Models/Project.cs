using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VEAP_ASPNET.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int LogStatus { get; set; }
        public string VcsUrl { get; set; }
    }
}