using Koduppgift.Data;
using Koduppgift.Dtos;
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

		public async Task<RoleDto> AddNewRole(RoleDto role)
			{
			var findRole = await _dataContext.Roles
				.Where(x => x.Name == role.RoleName)
				.SingleOrDefaultAsync();

			if (findRole != null)
				return null;

			var newRole = new Role
				{
				Name = role.RoleName
				};

			_dataContext.Roles.Add(newRole);
			_dataContext.SaveChanges();

			var roleDto = new RoleDto
				{
				RoleName = newRole.Name
				};

			return roleDto;
			}


		public async Task<RoleDto> GetRoleById(int id)
			{
			var findRole = await _dataContext.Roles.FindAsync(id);
			if (findRole == null)
				return null;

			var roleDto = new RoleDto
				{
				Id = findRole.Id,
				RoleName = findRole.Name
				};

			return roleDto;
			}

		public async Task<Role> GetUsersByRoleName(string roleName)
			{
			var users = await _dataContext.Roles
				.Where(x => x.Name == roleName)
				.Include(x => x.Users)
				.FirstOrDefaultAsync();

			if (users == null)
				return null;

			return users;
			}

		public async Task<RoleDto> UpdateRole(RoleDto role)
			{
			var findRole = await _dataContext.Roles.FindAsync(role.Id);
			if (findRole == null)
				return null;

			findRole.Id = role.Id;
			findRole.Name = role.RoleName;

			var roleDto = new RoleDto
				{
				Id = findRole.Id,
				RoleName = findRole.Name,
				};

			await _dataContext.SaveChangesAsync();

			return roleDto;

			}

		public async Task<Role> DeleteRole(int id)
			{
			var role = await _dataContext.Roles
				.Where(x => x.Id == id)
				.Include(x => x.Users)
				.FirstOrDefaultAsync();

			if (role == null)
				return null;

			_dataContext.Roles.Remove(role);
			await _dataContext.SaveChangesAsync();

			return role;

			}
		}
	}
