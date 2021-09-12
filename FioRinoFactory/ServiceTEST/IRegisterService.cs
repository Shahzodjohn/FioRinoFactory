using FioRinoFactory.DTOs;
using FioRinoFactory.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FioRinoFactory.ServiceTEST
{
    public interface IRegisterService
    {
        DmUser RegistrationUser(RegisterDTO dto);
    }
}
