using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarhammerCore.WebApi.Models.Response
{
    public class LoginResponse
    {
        public string token { get; }

        public LoginResponse(string _token)
        {
            token = _token;
        }
    }
}
