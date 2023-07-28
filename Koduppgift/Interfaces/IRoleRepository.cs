using Koduppgift.Dtos;
using Koduppgift.Models;

namespace Koduppgift.Interfaces
	{
	public interface IRoleRepository
		{
		Task<RoleDto> AddNewRole(RoleDto role);
		Task<RoleDto> GetRoleById(int id);
		Task<Role> GetUsersByRoleName(string roleName);
		Task<RoleDto> UpdateRole(RoleDto role);
		Task<Role> DeleteRole(int id);

		}
	}
