using System;
using System.Collections.Generic;
using System.Text;

namespace AhmadShop.DAL.DTO.Response
{
    public class RegisterResponse
    {
        public string Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
