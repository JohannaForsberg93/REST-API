using Azure.Core;
using Koduppgift.Data;
using Koduppgift.Dtos;
using Koduppgift.Interfaces;
using Koduppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace Koduppgift.Repositories
	{
	public class UserRepository : IUserRepository
		{
		private readonly DataContext _dataContext;

		public UserRepository(DataContext dataContext)
			{
			_dataContext = dataContext;
			}

		public async Task<UserDto> AddNewUser(UserDto user)
			{
			var newUser = new User
				{
				Name = user.UserName,
				Age = user.Age,
				RoleId = user.RoleId,
				};

			_dataContext.Users.Add(newUser);
			_dataContext.SaveChanges();

			var savedUser = new UserDto
				{
				UserName = newUser.Name,
				Age = newUser.Age,
				RoleId = newUser.RoleId,
				};

			return savedUser;
			}

		public async Task<User> GetUser(int id)
			{

			var user = await _dataContext.Users
			.Where(x => x.Id == id)
			.Include(x => x.Groups)
			.SingleAsync();

			if (user == null)
				return null;

			return user;
			}
		public async Task<List<User>> GetUserByRoleName(string roleName)
			{
			var users = await _dataContext.Users
				.Where(user => user.RoleId != null &&
							   _dataContext.Roles.Any(role => role.Id == user.RoleId && role.Name == roleName))
				.ToListAsync();

			return users;
			}

		public async Task<List<User>> GetUsersByGroupName(string groupName)
			{
			var users = await _dataContext.Users
				.Where(user => user.Groups.Any(group => group.Name == groupName))
				.ToListAsync();

			if (users == null)
				return null;

			return users;
			}

		public async Task<User> UpdateUser(UserDto updateUser, int groupId)
			{
			var user = await _dataContext.Users.FindAsync(updateUser.Id);
			if (user == null)
				return null;

			var checkGroup = await _dataContext.Groups.FindAsync(groupId);
			if (checkGroup == null)
				return null;

			user.Name = updateUser.UserName;
			user.Age = updateUser.Age;
			user.RoleId = updateUser.RoleId;
			user.Groups.Add(checkGroup);

			await _dataContext.SaveChangesAsync();

			return user;
			}

		public async Task<User> DeleteUser(int id)
			{
			var user = await _dataContext.Users
				.Where(x => x.Id == id)
				.Include(x => x.Groups)
				.SingleAsync();

			if (user == null)
				return null;

			_dataContext.Remove(user);
			_dataContext.SaveChanges();

			return user;
			}
		}
	}