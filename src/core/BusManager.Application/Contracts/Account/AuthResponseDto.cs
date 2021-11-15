using System;
using System.Collections.Generic;
using System.Text;

namespace BusManager.Application.Contracts.Account
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
