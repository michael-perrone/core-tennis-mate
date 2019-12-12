using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tennis_Mate.Models;

namespace Tennis_Mate.Data
{
    public interface IAuthRepository
    {
        Task<Instructor> RegisterInstructor(Instructor instructor, string password);
        Task<User> RegisterUser(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string email);

        Task<bool> InstructorExists(string email);
    }
}
