using Koduppgift.Models;

namespace Koduppgift.Interfaces
	{
	public interface IRoleRepository
		{
		Task<Role> AddNewRole(Role role);
		Task<Role> GetRoleById(int id);
		Task<Role> GetUsersByRoleName(string roleName);
		Task<Role> UpdateRole(Role role);
		Task<Role> DeleteRole(int id);

		}
	}
