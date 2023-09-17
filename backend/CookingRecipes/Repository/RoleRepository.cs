using CookingRecipes.Data;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;

namespace CookingRecipes.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRole(Role role)
        {
            _context.Roles.Add(role);
            return Save();
        }

        public Role GetRole(int id)
        {
            return _context.Roles.Where(r => r.Id == id).FirstOrDefault();
        }

        public Role GetRoleByUser(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(r => r.Role).FirstOrDefault();
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public ICollection<User> GetUsersByRoleId(int roleId)
        {
            return _context.Users.Where(u => u.Role.Id == roleId).ToList();
        }

        public bool RoleExists(int id)
        {
            return _context.Roles.Any(r => r.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
