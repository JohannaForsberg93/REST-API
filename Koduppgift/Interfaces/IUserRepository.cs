using Koduppgift.Models;
using Microsoft.AspNetCore.Mvc;

namespace Koduppgift.Interfaces
	{
	public interface IUserRepository
		{
		Task<User> AddNewUser(User user, int groupId);
		Task<User> GetUser(int id);
		Task<List<User>> GetUserByRoleName(string roleName);
		Task<List<User>> GetUsersByGroupName(string groupName);
		Task<User> UpdateUser(User user);
		Task<User> DeleteUser(int id);
		}
	}
