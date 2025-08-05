using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChain.Application.Dtos
{
    public class LoginResponseDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
