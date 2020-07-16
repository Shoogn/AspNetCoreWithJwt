using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWithJwt.Infrastructures
{
    public class AuthenticationSettings
    {
        // For Credentials
        public string Secret { get; set; }

        // after (...) days
        public int ExpirationDays { get; set; } 
    }
}
