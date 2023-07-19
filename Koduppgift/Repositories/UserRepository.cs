using Azure.Core;
using Koduppgift.Data;
using Koduppgift.Dtos;
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


		public async Task<UserDto> AddNewUser(UserCreateDto user)
			{
			var role = await _dataContext.Roles.FindAsync(user.RoleId);
			if (role == null)
				return null;

			var newUser = new User
				{
				Username = user.UserName,
				Age = user.Age,
				Role = role,
				Groups = new List<Group>()
				};

			_dataContext.Users.Add(newUser);
			await _dataContext.SaveChangesAsync();


			var createdUser = new UserDto
			{
				Id = newUser.Id,
				Username = newUser.Username,
				Age = newUser.Age,
				Role = new RoleDto
				{
					Id = newUser.Role.Id,
					RoleDtoName = newUser.Role.RoleName
				},
				Group = new List<GroupDto>()
			};

			return createdUser;
			}


		public async Task<UserDto> GetUser(int id)
		{

			var findUser = await _dataContext.Users
				.Include(x => x.Role)
				.SingleAsync(x => x.Id == id);

			if (findUser == null)
	return null;


			var user = new UserDto
			{
				Username = findUser.Username,
				Age = findUser.Age,
				Role = new RoleDto
				{
					Id = findUser.Role.Id,
					RoleDtoName = findUser.Role.RoleName
				}
			};

			return user;

			}

		public async Task<List<UserDto>> GetUserByRoleName(string roleName)
			{
			var users = await _dataContext.Users
				.Include(x => x.Role)
				.Where(x => x.Role.RoleName == roleName)
				.Select(x => new UserDto
					{
					Id = x.Id,
					Username = x.Username,
					Age = x.Age,
					Role = new RoleDto
						{
						Id = x.Id,
						RoleDtoName = x.Role.RoleName
						}
					})
				.ToListAsync();

			return users;
			}

		public async Task<List<UserDto>> GetUsersByGroupName(string groupName)
			{
			var users = await _dataContext.Users
				.Include(x => x.Groups)
				.Where(x => x.Groups.Any(g => g.GroupName == groupName))
				.Select(u => new UserDto
					{
					Id = u.Id,
					Username = u.Username,
					Age = u.Age,
					Group = u.Groups.Select(g => new GroupDto
						{
						Id = g.Id,
						GroupDtoName = g.GroupName
						}).ToList()
					})
				.ToListAsync();

			return users;
			}


		public async Task<UserDto> UpdateUser(UserDto payload)
		{
			var user = await _dataContext.Users.FindAsync(payload.Id);

			var role = await _dataContext.Roles.Where(x => x.Id == payload.Role.Id).FirstOrDefaultAsync();

			if (role == null)
				return null;

			user.Username = payload.Username;
			user.Age = payload.Age;
			user.Role = role;
			user.Groups = new List<Group>();

			await _dataContext.SaveChangesAsync();

			var updatedUser = new UserDto
			{
				Id = user.Id,
				Username = user.Username,
				Age = user.Age,
				Role = new RoleDto
				{
					Id = user.Role.Id,
					RoleDtoName = user.Role.RoleName
				},
				Group = new List<GroupDto>()
			};

			return updatedUser;
		}


		}

	}

