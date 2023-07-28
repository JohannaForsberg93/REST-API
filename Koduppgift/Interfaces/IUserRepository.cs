using Koduppgift.Dtos;
using Koduppgift.Models;
using Microsoft.AspNetCore.Mvc;

namespace Koduppgift.Interfaces
	{
	public interface IUserRepository
		{
		Task<UserDto> AddNewUser(UserDto user);
		Task<User> GetUser(int id);
		Task<List<User>> GetUserByRoleName(string roleName);
		Task<List<User>> GetUsersByGroupName(string groupName);
		Task<User> UpdateUser(UserDto user, int groupId);
		Task<User> DeleteUser(int id);
		}
	}
