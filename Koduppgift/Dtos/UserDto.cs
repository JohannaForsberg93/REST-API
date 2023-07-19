namespace Koduppgift.Dtos
	{
	public class UserDto
		{
		public int Id { get; set; }
		public string Username { get; set; }
		public int Age { get; set; }
		public RoleDto Role { get; set; }
		public ICollection<GroupDto> Group { get; set; } = new List<GroupDto>();
		}
	}
