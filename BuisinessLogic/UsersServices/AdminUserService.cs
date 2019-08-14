using DomainLayer.User;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BuisinessLogic.UsersServices
{
    public interface IAdminUserService
    {
        int getUserNum();

        //UserManager<User> UserManager { get; set; }
    }
    public class AdminUserService : IAdminUserService
    {
        public AdminUserService(UserManager<User> _userManager)
        {
            UserManager = _userManager;
        }
        public UserManager<User> UserManager { get; set; }

        public int getUserNum()
        {
            return Queryable.Count<DomainLayer.User.User>(UserManager.Users);
        }
    }
}