using CookingRecipes.Data.Enums;
using CookingRecipes.Models;

namespace CookingRecipes.Interfaces
{
    public interface IRoleRepository
    {
        Task<ICollection<Role>> GetRoles();
        Task<Role> GetRole(int id);
        Task<Role> GetRoleByUser(int userId);
        Task<ICollection<User>> GetUsersByRoleId(int roleId);
        bool RoleExists(int id);
        bool RoleTypeExists(RoleType roleType);
        Task<bool> CreateRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(Role role);
        Task<bool> Save();

    }
}
