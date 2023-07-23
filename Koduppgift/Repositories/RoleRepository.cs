using Koduppgift.Data;
using Koduppgift.Interfaces;
using Koduppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace Koduppgift.Repositories
	{
	public class RoleRepository : IRoleRepository
		{
		private readonly DataContext _dataContext;
		public RoleRepository(DataContext dataContext)
			{
			_dataContext = dataContext;
			}

		public async Task<Role> AddNewRole(Role role)
			{
			var checkRole = await _dataContext.Roles
				.Where(x => x.Name == role.Name)
				.SingleAsync();

			if (checkRole != null)
				return null;

			var newRole = new Role();

			newRole.Name = role.Name;
			newRole.Users = role.Users;

			_dataContext.Roles.Add(newRole);
			_dataContext.SaveChanges();

			return newRole;
			}

		public async Task<Role> GetRoleById(int id)
			{
			var role = await _dataContext.Roles.FindAsync(id);
			if (role == null)
				return null;

			return role;
			}

		public async Task<Role> GetUsersByRoleName(string roleName)
			{
			var users = await _dataContext.Roles
				.Where(x => x.Name == roleName)
				.Include(x => x.Users)
				.SingleAsync();

			if (users == null)
				return null;

			return users;
			}

		public async Task<Role> UpdateRole(Role role)
			{
			var updatedRole = await _dataContext.Roles.FindAsync(role.Id);
			if (updatedRole == null)
				return null;

			updatedRole.Name = role.Name;

			await _dataContext.SaveChangesAsync();

			return updatedRole;

			}

		public async Task<Role> DeleteRole(int id)
			{
			var role = await _dataContext.Roles.FindAsync(id);
			if (role == null)
				return null;

			_dataContext.Remove(role);
			await _dataContext.SaveChangesAsync();

			return role;

			}
		}
	}
