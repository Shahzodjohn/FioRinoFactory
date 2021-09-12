using FioRinoFactory.DTOs;
using FioRinoFactory.Models;
using FioRinoFactory.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FioRinoFactory.ServiceTEST
{
    public class RegisterService : IRegisterService
    {
        private readonly IUserRepository _repository;

        public RegisterService(IUserRepository repository)
        {
            _repository = repository;
        }

        public DmUser RegistrationUser(RegisterDTO dto)
        {
            var user = new DmUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                RoleId = dto.RoleId,
                PositionId = dto.PositionId
            };
           
            var IsUsedEmail = _repository.GetByEmail(dto.Email);
            if (IsUsedEmail != null)
            {
                return null;
            }

            return user;
        }
    }
}
