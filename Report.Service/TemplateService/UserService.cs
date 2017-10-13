using Report.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportAPI.Services
{
    public interface IUserService
    {
        UserDTO Authenticate(string username, string password);
        IEnumerable<UserDTO> GetAll();
        UserDTO GetById(int id);
        UserDTO Create(UserDTO user, string password);
        void Update(UserDTO user, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        public UserService()
        {

        }
        public UserDTO Authenticate(string _username, string _password)
        {
            if (_username == "no")
            {
                return new UserDTO();
            }
            else
            {
                return new UserDTO
                {
                    Id = 42,
                    FirstName = "Admin",
                    LastName = "Psc",
                    Password = "password",
                    Username = _username
                };
            }
        }

        public UserDTO Create(UserDTO user, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserDTO user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
