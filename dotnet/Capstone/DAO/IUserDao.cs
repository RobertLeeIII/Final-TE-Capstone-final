using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        IList<User> GetUsers();
        IList<User> GetUsersByPotluckId(int potluckId);
        InviteUser GetUserByEmail(string email);
        User GetHostByPotluckId(int potluckId);
        public int SendRegisteredInvitation(int potluckId, int userId);
        public int SendUnregisteredInvitation(int potluckId, string email);
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User CreateUser(string email, string username, string password, string role, bool dietaryRestriction);
    }
}
