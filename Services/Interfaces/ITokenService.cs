using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelacionTablas.Models;

namespace RelacionTablas.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}