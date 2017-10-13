using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Data.Users
{
    public interface IUserRepository
    {
        Task<UserDTO> AuthenticateAync(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private IUserContext _dbContext;

        public UserRepository(IUserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDTO> AuthenticateAync(string username, string password) => 
            await _dbContext.Users
                        .Where(u => u.Username == username)
                        .Where(u => u.Password == password)
                        .Select(au => new UserDTO
                        {
                            Id = au.Id,
                            FirstName = au.FirstName,
                            LastName = au.LastName,
                            Username = au.Username,
                            Password = au.Password
                        }).FirstOrDefaultAsync();
    }
}
