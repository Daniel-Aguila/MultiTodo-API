using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiAPI.Models
{
    public class JwtConstantsModel
    {
        public const string Issuer = "http://localhost:40063";
        public const string Audience = "ApiUser";
        public const string Key = "SecretKey10125779374235322";

        public const string AuthSchemes =
            "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;
    }
}
