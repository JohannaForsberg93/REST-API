using Azure.Core;
using Koduppgift.Data;
using Koduppgift.Interfaces;
using Koduppgift.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

		public async Task<User> AddNewUser(User user, int groupId)
			{
			var role = await _dataContext.Roles.FindAsync(user.RoleId);

			if (role == null)
				return null;

			var group = await _dataContext.Groups.FindAsync(groupId);
			if (group == null)
				return null;

			var newUser = new User
				{
				Name = user.Name,
				Age = user.Age,
				RoleId = user.RoleId,
				Groups = new List<Group> { group }
				};

			_dataContext.Users.Add(newUser);
			await _dataContext.SaveChangesAsync();

			return newUser;
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

		public async Task<User> UpdateUser(User updatedUser)
			{
			var user = await _dataContext.Users.FindAsync(updatedUser.Id);
			if (user == null)
				return null;

			user.Name = updatedUser.Name;
			user.Age = updatedUser.Age;
			user.RoleId = updatedUser.RoleId;
			user.Groups = updatedUser.Groups;

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

