using CookingRecipes.Data;
using CookingRecipes.Data.Enums;
using CookingRecipes.Interfaces;
using CookingRecipes.Models;
using Microsoft.EntityFrameworkCore;

namespace CookingRecipes.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            return await Save();
        }

        public async Task<bool> DeleteRole(Role role)
        {
            _context.Remove(role);
            return await Save();
        }

        public async Task<Role> GetRole(int id)
        {
            return await _context.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Role> GetRoleByUser(int userId)
        {
            return await _context.Users.Where(u => u.Id == userId).Select(r => r.Role).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Role>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<ICollection<User>> GetUsersByRoleId(int roleId)
        {
            return await _context.Users.Where(u => u.Role.Id == roleId).ToListAsync();
        }

        public bool RoleExists(int id)
        {
            return _context.Roles.Any(r => r.Id == id);
        }

        public bool RoleTypeExists(RoleType roleType)
        {
            return _context.Roles.Where(r => r.RoleType == roleType).Any();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRole(Role role)
        {
            _context.Update(role);
            return await Save();
        }
    }
}
