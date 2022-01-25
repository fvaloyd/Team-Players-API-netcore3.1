using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelacionTablas.Dtos
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public UserRegisterDto()
        {
            CreateDate = DateTime.Now;
        }
    }
}