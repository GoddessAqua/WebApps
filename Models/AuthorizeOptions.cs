using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskForIronWaterStudio.Models
{
    public class AuthorizeOptions
    {
        public const string AuthorizeInfo = "AuthorizeInfo";
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
