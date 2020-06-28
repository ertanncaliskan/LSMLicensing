using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSMLicensing
{
    public class Settings
    {
        public string DatabaseConnection { get; set; }
        public string SocketUrl { get; set; }
        public string SocketSecret { get; set; }
        public int SocketInterval { get; set; }
    }
}
