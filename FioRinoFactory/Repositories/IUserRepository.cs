using FioRinoFactory.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FioRinoFactory.Repositories
{
     public interface IUserRepository
    {
        DmUser Create(DmUser user);
        DmUser GetByEmail(string email);
        DmUser GetById(int Id);
    }
}
