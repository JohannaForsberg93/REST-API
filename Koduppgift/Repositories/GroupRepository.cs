using Koduppgift.Data;
using Koduppgift.Interfaces;
using Koduppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace Koduppgift.Repositories
	{
	public class GroupRepository : IGroupRepository
		{
		private readonly DataContext _dataContext;

		public GroupRepository(DataContext dataContext)
			{
			_dataContext = dataContext;
			}

		public async Task<Group> AddNewGroup(Group group)
			{
			var checkGroupName = await _dataContext.Groups
				.Where(x => x.Name == group.Name)
				.FirstOrDefaultAsync();

			if (checkGroupName != null)
				return null;

			var newGroup = new Group
				{
				Name = group.Name,
				};

			_dataContext.Groups.Add(newGroup);
			_dataContext.SaveChanges();

			return newGroup;
			}

		public async Task<Group> GetGroupById(int id)
			{
			var group = await _dataContext.Groups.FindAsync(id);
			if (group == null)
				return null;

			return group;
			}

		public async Task<List<User>> GetUsersByGroupId(int id)
			{
			var users = await _dataContext.Users
				.Where(user => user.Groups.Any(group => group.Id == id)).ToListAsync();

			if (users == null)
				return null;

			return users;
			}

		public async Task<Group> UpdateGroup(Group group)
			{
			var findGroup = await _dataContext.Groups.FindAsync(group.Id);

			if (findGroup == null)
				return null;

			findGroup.Id = group.Id;
			findGroup.Name = group.Name;

			_dataContext.SaveChanges();

			return findGroup;
			}

		public async Task<Group> DeleteGroup(int id)
			{
			var group = await _dataContext.Groups.FindAsync(id);
			if (group == null)
				return null;

			_dataContext.Remove(group);
			_dataContext.SaveChanges();

			return group;
			}

		}
	}
