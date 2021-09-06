using FioRinoFactory.Data;
using FioRinoFactory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FioRinoFactory.Repositories
{
    public  class UserRepository : IUserRepository
    {
        private readonly FioAndRinoContext _context;

        public UserRepository(FioAndRinoContext context)
        {
            _context = context;
        }

        public DmUser Create(DmUser user)
        {
            _context.DmUsers.AddAsync(user);
            _context.SaveChanges();
            return user;
        }

        public DmUser GetByEmail(string email)
        {
            return _context.DmUsers.FirstOrDefault(e => e.Email == email);
        }

        public DmUser GetById(int Id)
        {
            return _context.DmUsers.FirstOrDefault(u => u.Id == Id);
        }
    }
}
