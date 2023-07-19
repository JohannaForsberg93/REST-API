using Koduppgift.Dtos;
using Koduppgift.Models;
using Microsoft.AspNetCore.Mvc;

namespace Koduppgift.Interfaces
	{
	public interface IUserRepository
		{
			Task<UserDto> AddNewUser(UserCreateDto user);
			Task<UserDto> GetUser(int id);
			Task<List<UserDto>> GetUserByRoleName(string roleName);
			Task<List<UserDto>> GetUsersByGroupName(string groupName);
			Task<UserDto> UpdateUser(UserDto user);

		}
	}
