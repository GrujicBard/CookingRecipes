using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();
        Role GetRole(int id);
        Role GetRoleByUser(int userId);
        ICollection<User> GetUsersByRoleId(int roleId);
        bool RoleExists(int id);
    }
}
