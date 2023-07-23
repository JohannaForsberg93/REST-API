using Koduppgift.Models;

namespace Koduppgift.Interfaces
	{
	public interface IGroupRepository
		{
		Task<Group> AddNewGroup(Group group);
		Task<Group> GetGroupById(int id);
		Task<List<User>> GetUsersByGroupId(int id);
		Task<Group> UpdateGroup(Group group);
		Task<Group> DeleteGroup(int id);
		}
	}
